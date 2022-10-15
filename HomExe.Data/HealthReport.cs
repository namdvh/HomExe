using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public partial class HealthReport
    {
        public int UserId { get; set; }
        public string Problems { get; set; } = null!;
        public int HealthId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
