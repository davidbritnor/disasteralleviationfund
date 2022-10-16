using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE.Models
{
    public class PurchasedGoods
    {
        [Key]

        public int purchaseID { get; set; }
        public DateTime date { get; set; }
        public string item { get; set; }
        public string category { get; set; }
        public decimal price { get; set; }

    }
}
