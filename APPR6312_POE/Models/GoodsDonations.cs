using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class GoodsDonations
    {
        [Key]
        public int goodsID { get; set; }

        [Required]
        [DisplayName("Date")]
        public DateTime date { get; set; }
        [Required]
        [DisplayName("Number of Items")]
        public int numItems { get; set; }
        [Required]
        [DisplayName("Category")]
        public string category { get; set; }
        [Required]
        [DisplayName("Description")]
        public string description { get; set; }
        [Required]
        [DisplayName("Name")]
        public string name { get; set; }

    }
}
