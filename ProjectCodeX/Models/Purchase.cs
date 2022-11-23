using System;
using System.Collections.Generic;

namespace ProjectCodeX.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
            PurchLineItems = new HashSet<PurchLineItem>();
        }

        public int PurchId { get; set; }
        public string? UserId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PurchDate { get; set; }
        public string? Notes { get; set; }

        public virtual ICollection<PurchLineItem> PurchLineItems { get; set; }
    }
}
