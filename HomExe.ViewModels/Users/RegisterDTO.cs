using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomExe.ViewModels.Users
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; } = 1;
    }
}
