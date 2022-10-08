using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomExe.ViewModels.Users
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public decimal? Phone { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string Status { get; set; } = null!;
        public string RoleName { get; set; }
    }
}
