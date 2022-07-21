using BeetleX;
using BeetleX.EventArgs;
using PushFile.Messages.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdaterServer.Services.TcpServices.TcpServer;

namespace UpdaterServer.Services.TcpServices
{

	public class TcpServerService : ITcpServerService
	{
		private readonly IServer _serverDownload;
		private readonly IServer _serverLauncherUpdate;
		private readonly IServiceProvider _serviceProvider;

		public TcpServerService(IServiceProvider serviceProvider)
		{
			_serverDownload = SocketFactory.CreateTcpServer(new TcpServerDownloadHandler(serviceProvider), new ProtobufPacket());
			_serverDownload.Options.LogLevel = LogType.Warring;
			_serverDownload.Options.BufferSize = 1024 * 8;
			_serverDownload.Options.DefaultListen.Port = 9092;

			_serverLauncherUpdate = SocketFactory.CreateTcpServer(new TcpServerLauncherUpdateHandler(), new ProtobufPacket());
			_serverLauncherUpdate.Options.LogLevel = LogType.Warring;
			_serverLauncherUpdate.Options.BufferSize = 1024 * 8;
			_serverLauncherUpdate.Options.DefaultListen.Port = 9093;
			_serviceProvider = serviceProvider;
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
				_serverLauncherUpdate.Open();
				Thread.Sleep(-1);
			});
		}
	}
}
