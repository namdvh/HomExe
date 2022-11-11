using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomExe.ViewModels.Pts
{
    public class CreatePtDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal Phone { get; set; }
        public int CategoryId { get; set; }
        public string LinkMeet { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dob { get; set; }
        public string Cover { get; set; }
        public string Address { get; set; }

    }
}
