using PushFile.Messages.TcpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Services.TcpServer
{
	public static class TcpServerExtensions
	{
		public static void UseTcpServer(this WebApplication app)
		{
			app.Services.GetRequiredService<ITcpServerService>().Run();
		}
	}
}
