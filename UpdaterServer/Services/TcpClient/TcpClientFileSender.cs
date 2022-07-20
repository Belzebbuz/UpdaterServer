using BeetleX;
using BeetleX.Buffers;
using BeetleX.Clients;
using PushFile.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Services.TcpClient
{
	public class TcpClientFileSender : ITcpClientFileSender
	{
		private readonly AsyncTcpClient _tcpClient;

		public TcpClientFileSender()
		{
			BufferPool.BUFFER_SIZE = 1024 * 8;
			_tcpClient = SocketFactory.CreateClient<AsyncTcpClient, ProtobufClientPacket>("localhost", 9090);
		}

		public async Task Send(string filePath)
		{
			var reader = new FileReader(filePath);
			_tcpClient["file"] = reader;
			var block = reader.Next();
			block.Completed = OnCompleted;
			await _tcpClient.Send(block);
		}
		private void OnCompleted(FileContentBlock e)
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
		}
	}
}
