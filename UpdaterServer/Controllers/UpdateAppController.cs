using Microsoft.AspNetCore.Mvc;
using PushFile.Messages.TcpServices.TcpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UpdateAppController : ControllerBase
	{
		private readonly ITcpClientFileSender _tcpClient;

		public UpdateAppController(ITcpClientFileSender tcpClient)
		{
			_tcpClient = tcpClient;
		}

		[HttpGet("{appName}")]
		public async Task<string> CheckUpdateAsync(string appName)
		{
			await Task.Run(() => _tcpClient.Send(@"1234234.zip"));
			return appName;
		}
	}
}
