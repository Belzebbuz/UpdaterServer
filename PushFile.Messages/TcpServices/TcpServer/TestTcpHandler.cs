using BeetleX;
using BeetleX.EventArgs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushFile.Messages.TcpServices.TcpServer
{
	public class TestTcpHandler : ServerHandlerBase
	{
		public override void Connected(IServer server, ConnectedEventArgs e)
		{
			base.Connected(server, e);
		}
		protected override void OnReceiveMessage(IServer server, ISession session, object message)
		{
			string serviceName = "";
			if (message is FileContentBlock msg)
			{
				serviceName = Encoding.UTF8.GetString(msg.Data);
			}

			var reader = new FileReader(@"G:\CProj\UpdaterServer\UpdaterServer\1234234.zip");
			server["file"] = reader;
			var block = reader.Next();
			block.Completed = (block) =>
			{
				OnCompleted(session, server);
			};
			server.Send(block, session);
			base.OnReceiveMessage(server, session, message);
		}

		private void OnCompleted(ISession session, IServer server)
		{
			Task.Run(() =>
			{
				var reader = (FileReader)server["file"];
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
				Console.WriteLine(reader.Index);
			});
		}
	}
}
