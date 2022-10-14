using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public class Recipee
    {
        public int RecipeId { get; set; }
        public string Pictures { get; set; } = null!;
        public string Recipe { get; set; } = null!;
        public int CategoryId { get; set; }

        public virtual RecipeeCategory Category { get; set; } = null!;
    }
}
