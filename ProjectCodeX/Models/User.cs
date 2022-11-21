using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ProjectCodeX.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserGUID { get; set; } = "";
        public string FName { get; set; } = "";
        public string LName  { get; set; } = "";
        public string Address { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string PayMethod { get; set; } = "";
        public DateTime NextBillDate { get; set; } = DateTime.Now;
    }
}
