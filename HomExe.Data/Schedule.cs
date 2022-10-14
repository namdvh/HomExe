using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public string Date { get; set; } = null!;
        public int PtId { get; set; }

        public virtual Pt ScheduleNavigation { get; set; } = null!;
    }
}
