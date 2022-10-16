using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class Admin
    {
        [Key]

        public string username { get; set; }
        public string password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string status { get; set; }
    }
}
