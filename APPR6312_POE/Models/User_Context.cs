using Microsoft.EntityFrameworkCore;
using APPR6312_POE.Models;

namespace APPR6312_POE.Models
{
    public class User_Context : DbContext
    {
        public User_Context(DbContextOptions<User_Context> options) 
            : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }

        public static List<Users> staffObject = new List<Users>();

        public DbSet<APPR6312_POE.Models.MonetaryDonations>? MonetaryDonations { get; set; }

        public DbSet<APPR6312_POE.Models.Admin>? Admin { get; set; }

        public DbSet<APPR6312_POE.Models.GoodsDonations>? GoodsDonations { get; set; }    

        public DbSet<APPR6312_POE.Models.Disasters>? Disasters { get; set; }

        public DbSet<APPR6312_POE.Models.Inventory> Inventory { get; set; }

        public DbSet<APPR6312_POE.Models.PurchasedGoods> PurchasedGoods { get; set; }


    }
}
