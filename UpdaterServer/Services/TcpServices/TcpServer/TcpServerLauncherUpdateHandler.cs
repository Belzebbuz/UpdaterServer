using BeetleX;
using BeetleX.EventArgs;
using PushFile.Messages.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Services.TcpServices.TcpServer
{
	public class TcpServerLauncherUpdateHandler : ServerHandlerBase
	{
		public override void Connected(IServer server, ConnectedEventArgs e)
		{
			base.Connected(server, e);
		}
		protected override void OnReceiveMessage(IServer server, BeetleX.ISession session, object message)
		{
			string serviceName = "";
			if (message is FileContentBlock msg)
			{
				serviceName = Encoding.UTF8.GetString(msg.Data);
			}

			var reader = new FileReader(@"1234234.zip", "mes", "1.0.0");
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
