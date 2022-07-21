using BeetleX;
using BeetleX.EventArgs;
using PushFile.Messages;
using PushFile.Messages.TcpServices.TcpServer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushFile.Messages.TcpServices
{

	public class TcpServerService : ITcpServerService
	{
		private readonly IServer _serverDownload;
		private readonly IServer _serverTest;
		public TcpServerService()
		{
			_serverDownload = SocketFactory.CreateTcpServer(new TcpServerDownloadHandler(), new ProtobufPacket());
			_serverDownload.Options.LogLevel = LogType.Warring;
			_serverDownload.Options.BufferSize = 1024 * 8;
			_serverDownload.Options.DefaultListen.Port = 9092;

			_serverTest = SocketFactory.CreateTcpServer(new TestTcpHandler(), new ProtobufPacket());
			_serverTest.Options.LogLevel = LogType.Warring;
			_serverTest.Options.BufferSize = 1024 * 8;
			_serverTest.Options.DefaultListen.Port = 9093;
		}
		public void Run()
		{
			Task.Run(() =>
			{
				_serverDownload.Open();
				Thread.Sleep(-1);
			});
			Task.Run(() =>
			{
				_serverTest.Open();
				Thread.Sleep(-1);
			});
		}
	}
}
