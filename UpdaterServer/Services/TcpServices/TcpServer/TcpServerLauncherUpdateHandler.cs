using BeetleX;
using BeetleX.EventArgs;
using PushFile.Messages.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdaterServer.Domain;

namespace UpdaterServer.Services.TcpServices.TcpServer
{
	public class TcpServerLauncherUpdateHandler : ServerHandlerBase
	{
		private readonly IServiceProvider _serviceProvider;

		public TcpServerLauncherUpdateHandler(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		protected override void OnReceiveMessage(IServer server, BeetleX.ISession session, object message)
		{
			string serviceName = "";
			if (message is FileContentBlock msg)
			{
				serviceName = Encoding.UTF8.GetString(msg.Data);
			}
			using var scope = _serviceProvider.CreateScope();
			using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			var file = context.ProjectAssemblies.FirstOrDefault(x => x.Name.Contains("mes"));
			var reader = new FileReader(file.Path, file.Name);
			server[$"file{session.ID}"] = reader;
			var block = reader.Next();
			block.Completed = (block) =>
			{
				OnCompleted(session, server);
			};
			server.Send(block, session);
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
