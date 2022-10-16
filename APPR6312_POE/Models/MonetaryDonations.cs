using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class MonetaryDonations
    {
        public static Guid guid = Guid.NewGuid();
        string str = guid.ToString();

        [Key]
        public int monetaryID { get; set; }
        [Required]
        [DisplayName("Date")]
        public DateTime date { get; set; }
        [Required]
        [DisplayName("Amount")]
        public decimal amount { get; set; }
        [Required]
        [DisplayName("Name")]
        public string name { get; set; }

    }
}
