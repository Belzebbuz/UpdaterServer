namespace UpdaterServer.Services.TcpClient
{
	public interface ITcpClientFileSender
	{
		Task Send(string filePath);
	}
}