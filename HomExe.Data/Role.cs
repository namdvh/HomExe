using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public partial class Role
    {
        public Role()
        {
            Pts = new HashSet<Pt>();
        }

        public int RoleId { get; set; }
        public string Role1 { get; set; } = null!;

        public virtual ICollection<Pt> Pts { get; set; }
    }
}
