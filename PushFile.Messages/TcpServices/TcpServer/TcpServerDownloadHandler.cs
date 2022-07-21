using BeetleX;
using PushFile.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushFile.Messages.TcpServices.TcpServer
{
	public class TcpServerDownloadHandler : ServerHandlerBase
	{
		private readonly ConcurrentDictionary<string, FileTransfer> mFiles = new ConcurrentDictionary<string, FileTransfer>(StringComparer.OrdinalIgnoreCase);
		private readonly string _sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src");
		protected override void OnReceiveMessage(IServer server, ISession session, object message)
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
				}
			}
			base.OnReceiveMessage(server, session, message);
		}
	}
}
