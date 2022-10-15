using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public string Date { get; set; }
        public int PtId { get; set; }

        public virtual Pt ScheduleNavigation { get; set; } = null!;
    }
}
