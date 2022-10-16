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
    public class GoodsDonationsController : Controller
    {
        private readonly User_Context _context;

        public GoodsDonationsController(User_Context context)
        {
            _context = context;
        }

        // GET: GoodsDonations
        public async Task<IActionResult> Index()
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");
            return _context.GoodsDonations != null ? 
                          View(await _context.GoodsDonations.ToListAsync()) :
                          Problem("Entity set 'User_Context.GoodsDonations'  is null.");
        }

        // GET: GoodsDonations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");
            if (id == null || _context.GoodsDonations == null)
            {
                return NotFound();
            }

            var goodsDonations = await _context.GoodsDonations
                .FirstOrDefaultAsync(m => m.goodsID == id);
            if (goodsDonations == null)
            {
                return NotFound();
            }

            return View(goodsDonations);
        }

        // GET: GoodsDonations/Create
        public IActionResult Create()
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            return View();
        }

        // POST: GoodsDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("goodsID,date,numItems,category,description,name")] GoodsDonations goodsDonations, bool? anon)
        {
            ViewBag.name = HttpContext.Session.GetString("FirstName");
            ViewBag.surname = HttpContext.Session.GetString("LastName");

            var outputList = _context.GoodsDonations.Select(x => x.category).Distinct().ToList();
            ViewData["Categories"] = outputList;

            if (ModelState.IsValid)
            {
                if (anon == true)
                {
                    goodsDonations.name = "Anonymous";
                }
                else
                {
                    goodsDonations.name = ViewBag.name;
                }
                _context.Add(goodsDonations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(goodsDonations);
        }

        // GET: GoodsDonations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GoodsDonations == null)
            {
                return NotFound();
            }

            var goodsDonations = await _context.GoodsDonations.FindAsync(id);
            if (goodsDonations == null)
            {
                return NotFound();
            }
            return View(goodsDonations);
        }

        // POST: GoodsDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("goodsID,date,numItems,category,description,name")] GoodsDonations goodsDonations)
        {
            if (id != goodsDonations.goodsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goodsDonations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodsDonationsExists(goodsDonations.goodsID))
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
            return View(goodsDonations);
        }

        // GET: GoodsDonations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GoodsDonations == null)
            {
                return NotFound();
            }

            var goodsDonations = await _context.GoodsDonations
                .FirstOrDefaultAsync(m => m.goodsID == id);
            if (goodsDonations == null)
            {
                return NotFound();
            }

            return View(goodsDonations);
        }

        // POST: GoodsDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GoodsDonations == null)
            {
                return Problem("Entity set 'User_Context.GoodsDonations'  is null.");
            }
            var goodsDonations = await _context.GoodsDonations.FindAsync(id);
            if (goodsDonations != null)
            {
                _context.GoodsDonations.Remove(goodsDonations);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoodsDonationsExists(int id)
        {
          return (_context.GoodsDonations?.Any(e => e.goodsID == id)).GetValueOrDefault();
        }
    }
}
