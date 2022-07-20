using BeetleX;
using BeetleX.EventArgs;
using PushFile.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Services.TcpServer;

public class TcpServerHandler : ServerHandlerBase
{
	private readonly IServer _server;

	private readonly ConcurrentDictionary<string, FileTransfer> mFiles = new ConcurrentDictionary<string, FileTransfer>(StringComparer.OrdinalIgnoreCase);

	public TcpServerHandler()
	{
		_server = SocketFactory.CreateTcpServer(this, new ProtobufPacket());
		_server.Options.LogLevel = LogType.Warring;
		_server.Options.BufferSize = 1024 * 8;
		_server.Options.Listens.First().Port = 18018;
	}
	public void Run()
	{
		Task.Run(() =>
		{
			_server.Open();
			Thread.Sleep(-1);
		});
	}

	protected override void OnReceiveMessage(IServer server, BeetleX.ISession session, object message)
	{
		if (message is FileContentBlock block)
		{
			mFiles.TryGetValue(block.FileName, out FileTransfer value);
			if (block.Index == 0)
			{
				if (value != null)
				{
					value.Dispose();
				}
				value = new FileTransfer(block.FileName);
				mFiles[block.FileName] = value;
			}
			value.Stream.Write(block.Data, 0, block.Data.Length);
			if (block.Eof)
			{
				value.Dispose();
				mFiles.TryRemove(block.FileName, out value);
			}
		}
		Console.WriteLine(message);
	

		base.OnReceiveMessage(server, session, message);
	}
}
