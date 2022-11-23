using System.ComponentModel.DataAnnotations;

namespace ProjectCodeX.Models
{
    public class NewsPost
    {
        [Key]
        public int ArticleID { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Type { get; set; } = ""; //Enum?
        public string Link { get; set; } = ""; //Link type
        public int ViewCount { get; set; }
        public string Author { get; set; } = ""; //Author type
    }
}
