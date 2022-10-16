using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class Users
    {
        [Key]

        [Required]
        [DisplayName("Email")]
        public string email { get; set; }
        [Required]
        [DisplayName("Password")]
        public string password { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Cell Number")]
        public string CellNumber { get; set; }
        [Required]
        [DisplayName("Status")]
        public string status { get; set; } = "Pending";

    }
}
