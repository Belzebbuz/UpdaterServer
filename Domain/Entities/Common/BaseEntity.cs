using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
	public class BaseEntity : IAggregateRoot
	{
		public Guid Id { get; set; }
		public DateTime UpdateTime { get; set; }
		public string UserEmail { get; set; }
	}
}
