using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.Domain.Enties
{
	public class BaseEntity
	{
		public DateTime UpdateTime { get; set; }
		public string UserEmail { get; set; }
	}
}
