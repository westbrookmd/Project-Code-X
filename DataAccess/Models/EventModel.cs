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
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    [MaxLength(50)]
    public string Location { get; set; }
    [MaxLength(50)]
    public string EventType { get; set; }
    public int Attendees { get; set; }
    public double AmountRaised { get; set; }
    public double Cost { get; set; }
    [MaxLength(500)]
    public string Notes { get; set; }
}
