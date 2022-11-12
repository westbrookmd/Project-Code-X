using System.ComponentModel.DataAnnotations;

namespace ProjectCodeX.Models
{
    public class Event : IValidatableObject
    {
        public int EventID { get; set; }
        [Required]
        public DateTime Date { get; set; } //Consolidated into one value for model
        public DateTime Time { get; set; }
        [Required, MinLength(3)]
        public string Location { get; set; } = ""; //Location type
        [Required, MinLength(3)]
        public string Type { get; set; } = ""; //Enum?
        [Required]
        public int NumberOfAttendees { get; set; }
        [Required]
        public double AmountRaised { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required, MinLength(3)]
        public string Notes { get; set; } = "";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            return result;
        }
    }
}
