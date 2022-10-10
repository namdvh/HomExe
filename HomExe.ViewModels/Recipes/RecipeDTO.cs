using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomExe.ViewModels.Recipes
{
    public class RecipeDTO
    {
        public int? RecipeId { get; set; }
        public string Pictures { get; set; } 
        public string Recipe { get; set; } 
        public int CategoryId { get; set; }
    }
}
