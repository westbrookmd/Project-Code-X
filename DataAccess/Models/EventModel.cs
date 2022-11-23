using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;

public class EventModel
{
    public int EventID { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public TimeSpan Time { get; set; }
    [MaxLength(50)]
    [Required]
    public string Location { get; set; } = "";
    [MaxLength(50)]
    [Required]
    public string EventType { get; set; } = "";
    [Required]
    public int Attendees { get; set; }
    [Required]
    public double AmountRaised { get; set; }
    [Required]
    public double Cost { get; set; }
    [MaxLength(500)]
    [Required]
    public string Notes { get; set; } = "";

}
