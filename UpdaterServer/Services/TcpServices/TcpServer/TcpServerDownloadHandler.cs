using BeetleX;
using MediatR;
using PushFile.Messages.Infrastructure;
using System.Collections.Concurrent;
using UpdaterServer.Messages.Apps;
using UpdaterServer.Messages.ReleaseAssemblies;

namespace UpdaterServer.Services.TcpServices.TcpServer
{
	public class TcpServerDownloadHandler : ServerHandlerBase
	{
		private readonly ConcurrentDictionary<string, FileTransfer> mFiles = new ConcurrentDictionary<string, FileTransfer>(StringComparer.OrdinalIgnoreCase);
		private readonly string _sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assemblies");
		private readonly ISender _mediator;

		public TcpServerDownloadHandler(ISender mediator)
		{
			_mediator = mediator;
		}

		protected override void OnReceiveMessage(IServer server, BeetleX.ISession session, object message)
		{
			if (message is FileContentBlock block)
			{
				var project = _mediator.Send(new AppExistRequest(block.AppId)).Result;
				if (project != null || Path.GetExtension(block.FileName) != ".zip")
				{
					session.Dispose();
					return;
				}	
				
				var path = Path.Combine(_sourcePath,$"{block.AppId}{Path.GetExtension(block.FileName)}");
				mFiles.TryGetValue(block.FileName, out FileTransfer? value);

				if (block.Index == 0)
				{
					value = HandleFirstBlock(block, path, value);
				}

				value?.Stream.Write(block.Data, 0, block.Data.Length);

				if (block.Eof)
				{
					value?.Dispose();
					mFiles.TryRemove(block.FileName, out value);
					_mediator.Send(new CreateReleaseAssemblyRequest(block.AppId, path, Guid.NewGuid().ToString()));
				}
			}
			base.OnReceiveMessage(server, session, message);
		}

		private FileTransfer HandleFirstBlock(FileContentBlock block, string path, FileTransfer value)
		{
			if (value != null)
			{
				value.Dispose();
			}
			if (!Directory.Exists(_sourcePath))
				Directory.CreateDirectory(_sourcePath);

			value = new FileTransfer(path);
			mFiles[block.FileName] = value;
			return value;
		}

	}
}
