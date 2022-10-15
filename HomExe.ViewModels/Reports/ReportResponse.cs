using HomExe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomExe.ViewModels.Reports
{
    public class ReportResponse
    {
        public HealthReport HealthReport { get; set; }
        public List<Video>? Videos { get; set; }
    }
}
