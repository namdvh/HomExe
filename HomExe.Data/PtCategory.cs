using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public class PtCategory
    {
        public PtCategory()
        {
            Pts = new HashSet<Pt>();
        }

        public int CategoryId { get; set; }
        public string Category { get; set; } = null!;

        public virtual ICollection<Pt> Pts { get; set; }
    }
}
