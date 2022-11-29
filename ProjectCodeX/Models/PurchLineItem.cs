using System;
using System.Collections.Generic;

namespace ProjectCodeX.Models
{
    public partial class PurchLineItem
    {
        public int PlineId { get; set; }
        public int? PurchId { get; set; }
        public string PurchName { get; set; } = "";
        public int? Qnty { get; set; }
        public decimal? Price { get; set; }

        public virtual Purchase? Purch { get; set; }
    }
}
