using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectCodeX.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
            PurchLineItems = new HashSet<PurchLineItem>();
        }

        public int PurchId { get; set; }
        [MaxLength(500)]
        public string? UserId { get; set; }
        public decimal? Total { get; set; }
        public DateTime? PurchDate { get; set; }
        public string? Notes { get; set; }
        public string PurchName { get; set; } = "";
        public int? Qnty { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<PurchLineItem> PurchLineItems { get; set; }
    }
}
