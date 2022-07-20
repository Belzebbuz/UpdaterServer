using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdaterServer.Services.TcpClient;

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
			await _tcpClient.Send(@"C:\tracex builds\1234234.zip");
			return appName;
		}
	}
}
