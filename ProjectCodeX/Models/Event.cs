using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectCodeX.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public string? Location { get; set; }
        public string? EventType { get; set; }
        public int? Attendees { get; set; }
        public decimal? AmountRaised { get; set; }
        public decimal? Cost { get; set; }
        public string? Notes { get; set; }
    }
}
