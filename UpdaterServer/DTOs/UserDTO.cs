using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterServer.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
