namespace ProjectCodeX.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public DateTime Date { get; set; } //Consolidated into one value for model
        public string Location { get; set; } = ""; //Location type
        public string Type { get; set; } = ""; //Enum?
        public int NumberOfAttendees { get; set; }
        public double AmountRaised { get; set; }
        public double Cost { get; set; }
        public string Notes { get; set; }
    }
}
