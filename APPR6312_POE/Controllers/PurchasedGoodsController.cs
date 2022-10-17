using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APPR6312_POE.Models;

namespace APPR6312_POE.Controllers
{
    public class PurchasedGoodsController : Controller
    {
        private readonly User_Context _context;

        public PurchasedGoodsController(User_Context context)
        {
            _context = context;
        }

        // GET: PurchasedGoods
        public async Task<IActionResult> Index()
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            return View(await _context.PurchasedGoods.ToListAsync());
        }

        // GET: PurchasedGoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PurchasedGoods == null)
            {
                return NotFound();
            }

            var purchasedGoods = await _context.PurchasedGoods
                .FirstOrDefaultAsync(m => m.purchaseID == id);
            if (purchasedGoods == null)
            {
                return NotFound();
            }

            return View(purchasedGoods);
        }

        // GET: PurchasedGoods/Create
        public IActionResult Create()
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            return View();
        }

        // POST: PurchasedGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("purchaseID,date,numItems,category,description,name,price")] PurchasedGoods purchasedGoods)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            if (ModelState.IsValid)
            {
                Inventory i = new Inventory();

                i.Idate = purchasedGoods.date;
                i.InumItems = purchasedGoods.numItems;
                i.Icategory = purchasedGoods.category;
                i.Idescription = purchasedGoods.description;
                i.Iname = purchasedGoods.name;

                _context.Add(i);

                _context.Add(purchasedGoods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchasedGoods);
        }

        // GET: PurchasedGoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PurchasedGoods == null)
            {
                return NotFound();
            }

            var purchasedGoods = await _context.PurchasedGoods.FindAsync(id);
            if (purchasedGoods == null)
            {
                return NotFound();
            }
            return View(purchasedGoods);
        }

        // POST: PurchasedGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("purchaseID,date,numItems,category,description,name,price")] PurchasedGoods purchasedGoods)
        {
            if (id != purchasedGoods.purchaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchasedGoods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchasedGoodsExists(purchasedGoods.purchaseID))
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
            return View(purchasedGoods);
        }

        // GET: PurchasedGoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PurchasedGoods == null)
            {
                return NotFound();
            }

            var purchasedGoods = await _context.PurchasedGoods
                .FirstOrDefaultAsync(m => m.purchaseID == id);
            if (purchasedGoods == null)
            {
                return NotFound();
            }

            return View(purchasedGoods);
        }

        // POST: PurchasedGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PurchasedGoods == null)
            {
                return Problem("Entity set 'User_Context.PurchasedGoods'  is null.");
            }
            var purchasedGoods = await _context.PurchasedGoods.FindAsync(id);
            if (purchasedGoods != null)
            {
                _context.PurchasedGoods.Remove(purchasedGoods);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchasedGoodsExists(int id)
        {
          return _context.PurchasedGoods.Any(e => e.purchaseID == id);
        }
    }
}
