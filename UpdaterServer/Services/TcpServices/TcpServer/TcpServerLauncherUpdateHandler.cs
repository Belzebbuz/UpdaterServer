using BeetleX;
using BeetleX.EventArgs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PushFile.Messages.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdaterServer.Domain;
using UpdaterServer.Domain.Enties;
using UpdaterServer.Messages.Apps;
using UpdaterServer.Messages.ReleaseAssemblies;

namespace UpdaterServer.Services.TcpServices.TcpServer
{
	public class TcpServerLauncherUpdateHandler : ServerHandlerBase
	{
		private readonly ISender _mediator;

		public TcpServerLauncherUpdateHandler(ISender sender)
		{
			_mediator = sender;
		}

		protected override void OnReceiveMessage(IServer server, BeetleX.ISession session, object message)
		{
			if (message is FileContentBlock msg)
			{
				var file = _mediator.Send(new GetLastAppsReleaseAssemblyRequest(msg.AppId)).Result;
				if (file == null)
				{
					session.Dispose();
					return;
				}
				var reader = new FileReader(file.Path, file.Project.Id);
				server[$"file{session.ID}"] = reader;
				var block = reader.Next();
				block.Completed = (block) =>
				{
					OnCompleted(session, server);
				};
				server.Send(block, session);
			}
			base.OnReceiveMessage(server, session, message);
		}
		public void OnCompleted(BeetleX.ISession session, IServer server)
		{
			Task.Run(() =>
			{
				var reader = (FileReader)server[$"file{session.ID}"];
				if (!reader.Completed)
				{
					Task.Run(() =>
					{
						var block = reader.Next();
						block.Completed = (block) =>
						{
							OnCompleted(session, server);
						};
						server.Send(block, session);
					});
				}
			});
		}
	}
}
