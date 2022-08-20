using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.AuthDTO.ATH_005
{
	public class ATH_005: IRequest<IResponse>
	{
		public string UserId { get; set; }
		public List<SelectedRole> SelectedRoles { get; set; }
	}

	public class SelectedRole
	{
		public string Value { get; set; }
		public bool Check { get; set; }
	}

}
