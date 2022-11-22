using System;
using System.Collections.Generic;

namespace ProjectCodeX.Models
{
    public partial class Donation
    {
        public int DonationId { get; set; }
        public string UserId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? DonationDate { get; set; }
        public string? Notes { get; set; }
    }
}
