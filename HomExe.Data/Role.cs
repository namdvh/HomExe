using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public partial class Role
    {
        public Role()
        {
            Pts = new HashSet<Pt>();
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string Role1 { get; set; } = null!;

        public virtual ICollection<Pt> Pts { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
