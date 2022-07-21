using BeetleX;
using PushFile.Messages.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdaterServer.Domain;
using UpdaterServer.Domain.Enties;

namespace UpdaterServer.Services.TcpServices.TcpServer
{
	public class TcpServerDownloadHandler : ServerHandlerBase
	{
		private readonly ConcurrentDictionary<string, FileTransfer> mFiles = new ConcurrentDictionary<string, FileTransfer>(StringComparer.OrdinalIgnoreCase);
		private readonly string _sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src");
		private readonly IServiceProvider _serviceProvider;

		public TcpServerDownloadHandler(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
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
					if (!Directory.Exists(_sourcePath))
						Directory.CreateDirectory(_sourcePath);
					value = new FileTransfer($"{_sourcePath}\\{block.FileName}");
					mFiles[block.FileName] = value;
				}
				value.Stream.Write(block.Data, 0, block.Data.Length);
				if (block.Eof)
				{
					value.Dispose();
					mFiles.TryRemove(block.FileName, out value);
					using var scope = _serviceProvider.CreateScope();
					using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
					context.ProjectAssemblies.Add(new ProjectAssembly { Name = block.AppName, Path = $"{_sourcePath}\\{block.FileName}", Version = block.Version });
					context.SaveChanges();
				}
			}
			base.OnReceiveMessage(server, session, message);
		}
	}
}
