using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public partial class Video
    {
        public int VideoId { get; set; }
        public int PtId { get; set; }
        public string Link { get; set; } = null!;
        public virtual Pt Pt { get; set; } = null!;
    }
}
