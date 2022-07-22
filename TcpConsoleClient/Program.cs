using BeetleX;
using BeetleX.Buffers;
using BeetleX.Clients;
using PushFile.Messages.Infrastructure;
using System.Collections.Concurrent;
using System.Text;
using UpdaterServer.Services.TcpServices.TcpServer;

Console.ReadKey();
BufferPool.BUFFER_SIZE = 1024 * 8;

ConcurrentDictionary<string, FileTransfer?> mFiles = new ConcurrentDictionary<string, FileTransfer?>(StringComparer.OrdinalIgnoreCase);
string _sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wd");
var _tcpClient = SocketFactory.CreateClient<AsyncTcpClient, ProtobufClientPacket>("217.28.220.129", 33286);

_tcpClient.PacketReceive = (c, packet) =>
{
	HandlePacketRecieve(packet);
};

void HandlePacketRecieve(object message)
{
	if (message is FileContentBlock block)
	{
		mFiles.TryGetValue(block.FileName, out FileTransfer? value);
		if (block.Index == 0)
		{
			if (value != null)
			{
				value.Dispose();
			}
			if (!Directory.Exists(_sourcePath))
				Directory.CreateDirectory(_sourcePath);
			value = new FileTransfer($"{_sourcePath}\\{block.FileName}");
			mFiles[block.FileName] = value;
		}
		value?.Stream.Write(block.Data, 0, block.Data.Length);
		if (block.Eof)
		{
			value.Dispose();
			mFiles.TryRemove(block.FileName, out value);
		}
		Console.WriteLine(block.Index);
	}
}

await _tcpClient.Send(new FileContentBlock() { Data = Encoding.UTF8.GetBytes("mes")});

Thread.Sleep(-1);