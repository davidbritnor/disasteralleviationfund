using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APPR6312_POE.Models;
using Microsoft.AspNetCore.Http;

namespace APPR6312_POE.Controllers
{
    public class DisastersController : Controller
    {
        private readonly User_Context _context;

        public DisastersController(User_Context context)
        {
            _context = context;
        }

        // GET: Disasters
        public async Task<IActionResult> Index()
        {
            

            var sum = _context.Disasters.Sum(x => x.allocatedMoney);
            HttpContext.Session.SetString("Sum", sum.ToString());


            ViewBag.Sum = HttpContext.Session.GetString("Sum");
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");
            return _context.Disasters != null ? 
                          View(await _context.Disasters.ToListAsync()) :
                          Problem("Entity set 'User_Context.Disasters'  is null.");
        }

        // GET: Disasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            if (id == null || _context.Disasters == null)
            {
                return NotFound();
            }

            var disasters = await _context.Disasters
                .FirstOrDefaultAsync(m => m.disasterID == id);
            if (disasters == null)
            {
                return NotFound();
            }

            return View(disasters);
        }

        public async Task<IActionResult> Public()
        {
            return View();
        }

        // GET: Disasters/Create
        public IActionResult Purchase()
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");
            return View();
        }

        // POST: Disasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Purchase([Bind("disasterID,Start_Date,End_Date,Location,Description,aid,allocatedMoney,category,numItems")] Disasters disasters)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            

            

            if (ModelState.IsValid)
            {
                disasters.allocatedMoney = 0;
                _context.Add(disasters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disasters);
        }

        // GET: Disasters/Create
        public IActionResult Create()
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");
            return View();
        }

        // POST: Disasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("disasterID,Start_Date,End_Date,Location,Description,aid,allocatedMoney,category,numItems")] Disasters disasters)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

           

            if (ModelState.IsValid)
            {
                disasters.allocatedMoney = 0;
                _context.Add(disasters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disasters);
        }


        // GET: Disasters/Edit/5
        public async Task<IActionResult> AlloGoods(int? id)
        {
            
            if (id == null || _context.Disasters == null)
            {
                return NotFound();
            }

            var disasters = await _context.Disasters.FindAsync(id);
            if (disasters == null)
            {
                return NotFound();
            }
            return View(disasters);
        }

        // POST: Disasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlloGoods(int id, [Bind("disasterID,Start_Date,End_Date,Location,Description,aid,allocatedMoney,category,numItems")] Disasters disasters)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disasters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisastersExists(disasters.disasterID))
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
            return View(disasters);
        }




        // GET: Disasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            var outputList = _context.Inventory.Select(x => x.Icategory).Distinct().ToList();
            ViewData["Categories"] = outputList;

            if (id == null || _context.Disasters == null)
            {
                return NotFound();
            }

            var disasters = await _context.Disasters.FindAsync(id);
            if (disasters == null)
            {
                return NotFound();
            }
            return View(disasters);
        }

        // POST: Disasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("disasterID,Start_Date,End_Date,Location,Description,aid,allocatedMoney,category,numItems")] Disasters disasters)
        {
            GoodsDonations gd = new GoodsDonations();
            Disasters d = new Disasters();

            //var amount = _context.Disasters.Where(x => x.category == d.category).FirstOrDefault();

            //var totalleft = amount.numItems - d.numItems;

            // Get total of allocated money to disasters
            var allo = _context.Disasters.Sum(x => x.allocatedMoney);

            // Get total of monetary donations
            var Monetarysum = _context.MonetaryDonations.Sum(x => x.amount);

            // Get total of purchased goods
            var purchaseTotal = _context.PurchasedGoods.Sum(x => x.price);
            // Get remaining money left after subtracting allocated money
            var totalRemaining = Monetarysum - allo - purchaseTotal;


            if (id != disasters.disasterID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(disasters.allocatedMoney < totalRemaining)
                    { 
                    _context.Update(disasters);
                    await _context.SaveChangesAsync();
                    }
                    else if (disasters.allocatedMoney > totalRemaining)
                    {
                        ViewBag.Error = "Insufficent funds available";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisastersExists(disasters.disasterID))
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
            return View(disasters);
        }

        // GET: Disasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Disasters == null)
            {
                return NotFound();
            }

            var disasters = await _context.Disasters
                .FirstOrDefaultAsync(m => m.disasterID == id);
            if (disasters == null)
            {
                return NotFound();
            }

            return View(disasters);
        }

        // POST: Disasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Disasters == null)
            {
                return Problem("Entity set 'User_Context.Disasters'  is null.");
            }
            var disasters = await _context.Disasters.FindAsync(id);
            if (disasters != null)
            {
                _context.Disasters.Remove(disasters);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisastersExists(int id)
        {
          return (_context.Disasters?.Any(e => e.disasterID == id)).GetValueOrDefault();
        }
    }
}
