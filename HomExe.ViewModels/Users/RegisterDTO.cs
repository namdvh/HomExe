using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomExe.ViewModels.Users
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public decimal? Phone { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
    }
}
