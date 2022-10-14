using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public class RecipeeCategory
    {
        public RecipeeCategory()
        {
            Recipees = new HashSet<Recipee>();
        }

        public int CategoryId { get; set; }
        public string Category { get; set; } = null!;

        public virtual ICollection<Recipee> Recipees { get; set; }
    }
}
