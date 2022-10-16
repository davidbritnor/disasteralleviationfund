using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class Disasters
    {
        [Key]
        public int disasterID { get; set; }
        [Required]
        [DisplayName("Start Date")]
        public DateTime Start_Date { get; set; }
        [Required]
        [DisplayName("End Date")]
        public DateTime End_Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        [Required]
        [DisplayName("Aid Required")]
        public string aid { get; set; }
        [Required]
        [DisplayName("Allocated Money")]
        public decimal allocatedMoney { get; set; }
    }
}
