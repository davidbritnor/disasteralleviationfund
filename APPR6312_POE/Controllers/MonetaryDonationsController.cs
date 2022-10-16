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
    public class MonetaryDonationsController : Controller
    {
        private readonly User_Context _context;

        public MonetaryDonationsController(User_Context context)
        {
            _context = context;
        }

        // GET: MonetaryDonations
        public async Task<IActionResult> Index()
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");
            return _context.MonetaryDonations != null ? 
                          View(await _context.MonetaryDonations.ToListAsync()) :
                          Problem("Entity set 'User_Context.MonetaryDonations'  is null.");
        }

        // GET: MonetaryDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");
            if (id == null || _context.MonetaryDonations == null)
            {
                return NotFound();
            }

            var monetaryDonations = await _context.MonetaryDonations
                .FirstOrDefaultAsync(m => m.monetaryID == id);
            if (monetaryDonations == null)
            {
                return NotFound();
            }

            return View(monetaryDonations);
        }

        // GET: MonetaryDonations/Create
        public IActionResult Create()
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");
            return View();
        }

        // POST: MonetaryDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("monetaryID,date,amount,name")] MonetaryDonations monetaryDonations, bool? anon)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            

            if (ModelState.IsValid)
            {
                monetaryDonations.name = ViewBag.name = HttpContext.Session.GetString("FirstName");

                if (anon == true)
                {
                    monetaryDonations.name = "Anonymous";
                }


                _context.Add(monetaryDonations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                
            }
            return View(monetaryDonations);
        }

        // GET: MonetaryDonations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MonetaryDonations == null)
            {
                return NotFound();
            }

            var monetaryDonations = await _context.MonetaryDonations.FindAsync(id);
            if (monetaryDonations == null)
            {
                return NotFound();
            }
            return View(monetaryDonations);
        }

        // POST: MonetaryDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("monetaryID,date,amount,name")] MonetaryDonations monetaryDonations)
        {
            if (id != monetaryDonations.monetaryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monetaryDonations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonetaryDonationsExists(monetaryDonations.monetaryID))
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
            return View(monetaryDonations);
        }

        // GET: MonetaryDonations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MonetaryDonations == null)
            {
                return NotFound();
            }

            var monetaryDonations = await _context.MonetaryDonations
                .FirstOrDefaultAsync(m => m.monetaryID == id);
            if (monetaryDonations == null)
            {
                return NotFound();
            }

            return View(monetaryDonations);
        }

        // POST: MonetaryDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MonetaryDonations == null)
            {
                return Problem("Entity set 'User_Context.MonetaryDonations'  is null.");
            }
            var monetaryDonations = await _context.MonetaryDonations.FindAsync(id);
            if (monetaryDonations != null)
            {
                _context.MonetaryDonations.Remove(monetaryDonations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonetaryDonationsExists(int id)
        {
          return (_context.MonetaryDonations?.Any(e => e.monetaryID == id)).GetValueOrDefault();
        }
    }
}
