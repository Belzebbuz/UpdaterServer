using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Controllers
{
	[ApiController]
	public class BaseApiController : ControllerBase
	{
		private ISender _mediator = null!;

		protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
		protected string ReleasesPath 
		{ 
			get 
			{
				string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "releases");
				if (!Directory.Exists(path))
					Directory.CreateDirectory(path);
				return path;
			}
		}
	}
}
