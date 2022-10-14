using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public partial class Contract
    {
        public int UserId { get; set; }
        public int PtId { get; set; }
        public string CreatedDate { get; set; } = null!;
        public string EndDate { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual Pt Pt { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
