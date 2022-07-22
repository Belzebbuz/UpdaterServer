using BeetleX;
using BeetleX.Buffers;
using BeetleX.Clients;
using PushFile.Messages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Services.TcpServices.TcpClient
{
	public class TcpClientFileSender : ITcpClientFileSender
	{
		private readonly AsyncTcpClient _tcpClient;

		public TcpClientFileSender()
		{
			BufferPool.BUFFER_SIZE = 1024 * 8;
			_tcpClient = SocketFactory.CreateClient<AsyncTcpClient, ProtobufClientPacket>("localhost", 9092);
		}

		public void Send(string filePath)
		{
			var reader = new FileReader(filePath,"mes");
			_tcpClient["file"] = reader;
			var block = reader.Next();
			block.Completed = OnCompleted;
			_tcpClient.Send(block);
		}

		private void OnCompleted(FileContentBlock e)
		{
			Task.Run(() =>
			{
				var reader = (FileReader)_tcpClient["file"];
				if (!reader.Completed)
				{
					Task.Run(() =>
					{
						var block = reader.Next();
						block.Completed = OnCompleted;
						_tcpClient.Send(block);
					});
				}
				else
				{
					_tcpClient.DisConnect();
				}
				Console.WriteLine(reader.Index);
			});
		}
	}
}
