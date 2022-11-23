using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectCodeX.Models
{
    public partial class User : IdentityUser
    {
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PayMethod { get; set; }
        public DateTime? NextBillDate { get; set; }
    }
}
