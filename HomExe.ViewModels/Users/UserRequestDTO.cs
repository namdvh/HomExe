using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomExe.ViewModels.Users
{
    public class UserRequestDTO
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public decimal? Phone { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? Status { get; set; }
    }
}
