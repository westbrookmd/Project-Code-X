using System;
using System.Collections.Generic;

namespace ProjectCodeX.Models
{
    public partial class Member
    {
        public string UserId { get; set; }
        public decimal? Due { get; set; }
        public decimal? Balance { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
