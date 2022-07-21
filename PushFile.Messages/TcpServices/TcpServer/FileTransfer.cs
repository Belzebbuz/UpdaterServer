﻿namespace PushFile.Messages.TcpServices.TcpServer
{
	public class FileTransfer : IDisposable
	{
		public FileTransfer(string name)
		{
			Name = name;
			Stream = File.Open(name, FileMode.OpenOrCreate);
		}

		public string Name { get; set; }

		public Stream Stream { get; private set; }

		public void Dispose()
		{
			Stream?.Flush();
			Stream?.Dispose();

		}
	}
}