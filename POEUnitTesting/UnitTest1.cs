using Microsoft.EntityFrameworkCore;
using APPR6312_POE.Controllers;
using APPR6312_POE.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POEUnitTesting
{
    public class UnitTest1
    {
        private readonly User_Context _context;

        
        public UnitTest1()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<User_Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            _context = new User_Context(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task getDisasters_Return()
        {
            var disasters = await _context.Disasters.ToListAsync();
            var model = Assert.IsAssignableFrom<IEnumerable<Disasters>>(disasters);
        }

        [Fact]
        public async Task getMonetary_Return()
        {
            var monetary = await _context.MonetaryDonations.ToListAsync();
            var model = Assert.IsAssignableFrom<IEnumerable<MonetaryDonations>>(monetary);
        }

        [Fact]
        public async Task getGoods_Return()
        {
            var goods = await _context.GoodsDonations.ToListAsync();
            var model = Assert.IsAssignableFrom<IEnumerable<GoodsDonations>>(goods);
        }

        [Fact]
        public async Task getInventory_Return()
        {
            var invetory = await _context.Inventory.ToListAsync();
            var model = Assert.IsAssignableFrom<IEnumerable<Inventory>>(invetory);
        }

        [Fact]
        public async Task getUsers_Return()
        {
            var user = await _context.Users.ToListAsync();
            var model = Assert.IsAssignableFrom<IEnumerable<Users>>(user);
        }

        [Fact]
        public async Task getPurchased_Return()
        {
            var purchased = await _context.PurchasedGoods.ToListAsync();
            var model = Assert.IsAssignableFrom<IEnumerable<PurchasedGoods>>(purchased);
        }


    }
}