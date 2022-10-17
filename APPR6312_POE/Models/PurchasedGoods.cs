using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class PurchasedGoods
    {
        [Key]

        public int purchaseID { get; set; }
        [DisplayName("Date")]
        public DateTime date { get; set; }
        [DisplayName("Number of Items")]
        public int numItems { get; set; }
        [DisplayName("Category")]
        public string category { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }
        [DisplayName("Name")]
        public string name { get; set; }
        [DisplayName("Price")]
        public decimal price { get; set; }

    }
}
