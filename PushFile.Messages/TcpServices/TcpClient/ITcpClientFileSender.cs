namespace PushFile.Messages.TcpServices.TcpClient
{
	public interface ITcpClientFileSender
	{
		void Send(string filePath);
	}
}