using System;
using System.Collections.Generic;

namespace ProjectCodeX.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserGuid { get; set; } = null!;
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? PayMethod { get; set; }
        public DateTime? NextBillDate { get; set; }
    }
}
