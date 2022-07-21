namespace UpdaterServer.Services.TcpServices.TcpClient
{
	public interface ITcpClientFileSender
	{
		void Send(string filePath);
	}
}