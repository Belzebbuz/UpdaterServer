using System.Collections.Concurrent;

namespace UpdaterServer.Services.Common;

public interface IBlockFileSender
{
	public Task UploadChunk(FileBlock block, string destPath);
}

public class FileBlock
{
	public Guid StreamId { get; set; }
	public byte[] Data { get; set; }
	public int Index { get; set; }
	public bool EndOfStream { get; set; }
}
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
public class BlockFileSender : IBlockFileSender
{
	private readonly ConcurrentDictionary<string, FileTransfer> mFiles = new();

	public async Task UploadChunk(FileBlock block, string destPath)
	{
		mFiles.TryGetValue(block.StreamId.ToString(), out FileTransfer? value);
		if (block.Index == 0)
		{
			value = CreateFirstBlock(block, destPath, value);
		}

		await value.Stream.WriteAsync(block.Data, 0, block.Data.Length);

		if (block.EndOfStream)
		{
			value?.Dispose();
			mFiles.TryRemove(block.StreamId.ToString(), out value);
		}
	}

	private FileTransfer CreateFirstBlock(FileBlock block, string destPath, FileTransfer value)
	{
		if (value != null)
			value.Dispose();
		var diestctory = Path.GetDirectoryName(destPath);
		if (!Directory.Exists(diestctory))
			Directory.CreateDirectory(diestctory);

		value = new FileTransfer(destPath);
		mFiles[block.StreamId.ToString()] = value;
		return value;
	}
}
