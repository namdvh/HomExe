using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomExe.ViewModels.Contracts
{
    public class ContractDTO
    {
        public int? ContractId { get; set; }
        public int UserId { get; set; }
        public int PtId { get; set; }
        public string CreatedDate { get; set; } 
        public string EndDate { get; set; }
        public string Status { get; set; } 
    }
}
