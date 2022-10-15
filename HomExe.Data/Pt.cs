using System;
using System.Collections.Generic;

namespace HomExe.Data
{
    public partial class Pt
    {
        public Pt()
        {
            Contracts = new HashSet<Contract>();
            Videos = new HashSet<Video>();
        }

        public int PtId { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public decimal Phone { get; set; }
        public int CategoryId { get; set; }
        public string LinkMeet { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Dob { get; set; }
        public int? Rating { get; set; }
        public string? Cover { get; set; }
        public string? Certificate { get; set; }
        public string? Address { get; set; }
        public string? Schedules { get; set; }

        public virtual PtCategory Category { get; set; } = null!;
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
