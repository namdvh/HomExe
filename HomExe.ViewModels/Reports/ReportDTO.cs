using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomExe.ViewModels.Reports
{
    public class ReportDTO
    {
        public int UserId { get; set; }
        public string Problems { get; set; } 
        public string Target { get; set; } 
        public int? HealthId { get; set; }
    }
}
