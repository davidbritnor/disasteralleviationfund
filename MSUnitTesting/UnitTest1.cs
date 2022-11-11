using APPR6312_POE.Controllers;
using APPR6312_POE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MSUnitTesting
{
    [TestClass]
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

            Users();
        }

        

        [TestMethod]
        public void LoginPass()
        {

            string email1 = "reece@gmail.com";
            string password1 = "1234";

            string email2 = "davidbritnor@gmail.com";
            string password2 = "12345";

            UsersController usersController = new UsersController(_context);

            bool user1 = usersController.loginTest(email1, password1);
            bool user2 = usersController.loginTest(email2, password2);

            Assert.AreEqual(false, user1);
            Assert.AreEqual(false, user2);




        }

        [TestMethod]
        public void Login_Fail()
        {
            string email1 = "reece@gmail.com";
            string password1 = "1234";

            string email2 = "davidbritnor@gmail.com";
            string password2 = "12345";

            UsersController usersController = new UsersController(_context);

            bool user1 = usersController.loginTest(email1, password1);
            bool user2 = usersController.loginTest(email2, password2);

            Assert.AreNotEqual(true, user1);
            Assert.AreNotEqual(true, user2);
        }

        public void Users()
        {
            Users users1 = new Users();

            users1.email = "reece@gmail.com";
            users1.password = "81dc9bdb52d04dc20036dbd8313ed055";
            users1.FirstName = "Reece";
            users1.LastName = "Wanvig";
            users1.CellNumber = "1234456";
            users1.status = "Approved";
           
            _context.Users.Add(users1);

            Users users2 = new Users();
            users2.email = "davidbritnor@gmail.com";
            users2.password = "827ccb0eea8a706c4c34a16891f84e7b";
            users2.FirstName = "David";
            users2.LastName = "Britnor";
            users2.CellNumber = "0725175563";
            users2.status = "Approved";

            _context.Users.Add(users2);

            _context.SaveChanges();
        }
    }
}