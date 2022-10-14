using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public decimal? Phone { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string Status { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Contract? Contract { get; set; }
    }
}
