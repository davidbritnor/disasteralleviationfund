﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APPR6312_POE.Models;
using System.Security.Cryptography;
using System.Text;

namespace APPR6312_POE.Controllers
{
    public class UsersController : Controller
    {
        private readonly User_Context _context;

        public UsersController(User_Context context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var email = HttpContext.Session.GetString("Email");
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            if (String.IsNullOrWhiteSpace(email))
            {
                return _context.Users != null ?
                              View(await _context.Users.ToListAsync()) :
                              Problem("Entity set 'User_Context.Users'  is null.");
            }

            return _context.Users != null ?
              View(await _context.Users.Where(x => x.email == email).ToListAsync()) :
              Problem("Entity set 'User_Context.Users'  is null.");
        }


        public async Task<IActionResult> Admin()
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            return _context.Users != null ?
                        View(await _context.Users.Where(x => x.status != "Admin").ToListAsync()) :
                        Problem("Entity set 'User_Context.Users'  is null.");
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Public()
        {
            var itemCount = _context.GoodsDonations.Sum(x => x.numItems);

            // Get total of allocated money to disasters
            var allo = _context.Disasters.Sum(x => x.allocatedMoney);

            // Get total of monetary donations
            var Monetarysum = _context.MonetaryDonations.Sum(x => x.amount);

            // Get total of purchased goods
            var purchaseTotal = _context.PurchasedGoods.Sum(x => x.price);
            // Get remaining money left after subtracting allocated money
            var totalRemaining = Monetarysum - allo - purchaseTotal;

            HttpContext.Session.SetString("MonetarySum", totalRemaining.ToString());
            HttpContext.Session.SetString("TotalMon", Monetarysum.ToString());
            HttpContext.Session.SetString("TotalGoods", itemCount.ToString());


            ViewBag.MonetaryTotal = HttpContext.Session.GetString("TotalMon");
            ViewBag.MonetarySum = HttpContext.Session.GetString("MonetarySum");
            ViewBag.TotalGoods = HttpContext.Session.GetString("TotalGoods");


            return View();
        }
        public IActionResult Test()
        {
            return View();
        }


        public bool validEmail(string email)
        {
            Users u = _context.Users.Find(email);

            if(u == null)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var e_password = GetMD5(password);

            HttpContext.Session.SetString("password", e_password);
            ViewBag.password = HttpContext.Session.GetString("password");

            var admin = _context.Users.ToList();


            if (loginAdmin(email, password) != false)
            {
                validEmail(email);
                Users u = _context.Users.Find(email);

                setSessions(u);

                return RedirectToAction("Admin");
            }
            else
            
            if (loginUser(email, password) != false)
            {
                validEmail(email);
                Users u = _context.Users.Find(email);

                setSessions(u);

                //ViewBag.Sum = HttpContext.Session.GetString("Sum");

                return RedirectToAction("Index" , "Home");
            }
            else
            {
                ViewBag.Error = "Invalid Credentials, Please enter your details again";
                return View();
            }
                       
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Approve(string? email)
        {
            var obj = _context.Users.Find(email);

            if(obj == null)
            {
                return RedirectToAction("Admin");
            }

            obj.status = "Approved";
            _context.Update(obj);
            await _context.SaveChangesAsync();

            return RedirectToAction("Admin");
            
        }

        [HttpPost]
        public async Task<IActionResult> Deny(string? email)
        {
            var obj = _context.Users.Find(email);

            if (obj == null)
            {
                return RedirectToAction("Admin");
            }

            obj.status = "Denied";
            _context.Update(obj);
            await _context.SaveChangesAsync();

            return RedirectToAction("Admin");

        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.email == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        public bool testRegister(Users user)
        {            

            if(String.IsNullOrWhiteSpace(user.email) || String.IsNullOrWhiteSpace(user.password) ||
                String.IsNullOrWhiteSpace(user.FirstName) || String.IsNullOrWhiteSpace(user.LastName) || String.IsNullOrWhiteSpace(user.CellNumber))
            {

                ViewBag.Error = "Please fill in all required fields";
                return false;

                
            }
            else
            {
                Users u = new Users();

                u.email = user.email;
                u.password = GetMD5(user.password);
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.CellNumber = user.CellNumber;
                u.status = user.status;

                return true;
            }

            
           
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("email,password,FirstName,LastName,CellNumber,status")] Users users)
        {
            if (ModelState.IsValid)
            {
                testRegister(users);

                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("email,password,FirstName,LastName,CellNumber,status")] Users users)
        {
            

            if (id != users.email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.name = HttpContext.Session.GetString("FirstName");
                    ViewBag.surname = HttpContext.Session.GetString("LastName");

                    users.password = HttpContext.Session.GetString("password");
                    users.status = "Approved";
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.email == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'User_Context.Users'  is null.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(string id)
        {
          return (_context.Users?.Any(e => e.email == id)).GetValueOrDefault();
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        //public bool loginAdmin(string email, string password)
        //{
        //    var e_password = GetMD5(password);

        //    HttpContext.Session.SetString("password", e_password);
        //    ViewBag.password = HttpContext.Session.GetString("password");

        //    var admin = _context.Users.Where(s => s.email == email && s.password == e_password && s.status == "Admin").FirstOrDefault();

        //    HttpContext.Session.SetString("FirstName", email);

        //    if(admin.status == "Admin")
        //    {

        //    }

        //    return true;       
        //}

        //public bool loginUser(string email, string password)
        //{
        //    var e_password = GetMD5(password);

        //    HttpContext.Session.SetString("password", e_password);
        //    ViewBag.password = HttpContext.Session.GetString("password");

        //    var obj = _context.Users.Where(a => a.email == email && a.password == e_password && a.status == "Approved").FirstOrDefault();

        //    if(obj.status == "Approved")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    //HttpContext.Session.SetString("Email", email);
        //    //ViewBag.email = HttpContext.Session.GetString("Email");
        //    //HttpContext.Session.SetString("FirstName", obj.FirstName);
        //    //HttpContext.Session.SetString("LastName", obj.LastName);
           
        //}

        public void setSessions(Users user)
        {
            HttpContext.Session.SetString("Email", user.email);
            HttpContext.Session.SetString("FirstName", user.FirstName);
            HttpContext.Session.SetString("LastName", user.LastName);
            HttpContext.Session.SetString("CellNumber", user.CellNumber);
            HttpContext.Session.SetString("Status", user.status);

        }

        public bool loginAdmin(string email, string password)
        {
            var e_password = GetMD5(password);

            //HttpContext.Session.SetString("password", e_password);
            //ViewBag.password = HttpContext.Session.GetString("password");

            var obj = _context.Users.Where(a => a.email == email && a.password == e_password).FirstOrDefault();

            if(obj.status == "Admin")
            {         

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool loginUser(string email, string password)
        {
            var e_password = GetMD5(password);

            var obj = _context.Users.Where(a => a.email == email && a.password == e_password).FirstOrDefault();

            if(obj.status == "Approved")
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
