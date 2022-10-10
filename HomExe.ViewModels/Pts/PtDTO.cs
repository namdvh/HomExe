using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomExe.ViewModels.Pts
{
    public class PtDTO
    {
        public int? PtId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal? Phone { get; set; }
        public int CategoryId { get; set; }
        public string LinkMeet { get; set; }
        public string Status { get; set; }
    }
}
