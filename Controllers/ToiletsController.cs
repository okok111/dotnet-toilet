using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToiletApp.Data;
using ToiletApp.Models;

namespace ToiletApp.Controllers
{
    public class ToiletsController : Controller
    {
        private readonly ToiletAppContext _context;

        public ToiletsController(ToiletAppContext context)
        {
            _context = context;
        }

        // GET: Toilets
        public async Task<IActionResult> Index()
        {
            var toiletAppContext = _context.Toilets.Include(t => t.Areas);
            return View(await toiletAppContext.ToListAsync());
        }

        // GET: Toilets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toilets = await _context.Toilets
                .Include(t => t.Areas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toilets == null)
            {
                return NotFound();
            }

            return View(toilets);
        }

        // GET: Toilets/Create
        public IActionResult Create()
        {
            ViewData["AreasId"] = new SelectList(_context.Areas, "Id", "Id");
            return View();
        }

        // POST: Toilets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Advise,Url,Lat,Lng,AreasId")] Toilets toilets)
        {
                _context.Add(toilets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Toilets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toilets = await _context.Toilets.FindAsync(id);
            if (toilets == null)
            {
                return NotFound();
            }
            ViewData["AreasId"] = new SelectList(_context.Areas, "Id", "Id", toilets.AreasId);
            return View(toilets);
        }

        // POST: Toilets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Advise,Url,Lat,Lng,AreasId")] Toilets toilets)
        {
            if (id != toilets.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(toilets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToiletsExists(toilets.Id))
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

        // GET: Toilets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toilets = await _context.Toilets
                .Include(t => t.Areas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toilets == null)
            {
                return NotFound();
            }

            return View(toilets);
        }

        // POST: Toilets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toilets = await _context.Toilets.FindAsync(id);
            if (toilets != null)
            {
                _context.Toilets.Remove(toilets);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToiletsExists(int id)
        {
            return _context.Toilets.Any(e => e.Id == id);
        }
    }
}
