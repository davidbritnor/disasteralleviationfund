using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class PurchasedGoods
    {
        [Key]

        public int purchaseID { get; set; }
        public DateTime date { get; set; }
        public int numItems { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }

    }
}
