using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class Inventory
    {
        [Key]
        public int IgoodsID { get; set; }

        [Required]
        [DisplayName("Date")]
        public DateTime Idate { get; set; }
        [Required]
        [DisplayName("Number of Items remaining")]
        public int InumItems { get; set; }
        [Required]
        [DisplayName("Category")]
        public string Icategory { get; set; }
        [Required]
        [DisplayName("Description")]
        public string Idescription { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Iname { get; set; }
    }
}
