using System;
using System.Collections.Generic;

namespace ProjectCodeX.Models
{
    public partial class Contact
    {
        public int ContactId { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string? Company { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
