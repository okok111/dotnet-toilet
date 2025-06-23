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
        public class CommentsController : Controller
        {
            private readonly ToiletAppContext _context;

            public CommentsController(ToiletAppContext context)
            {
                _context = context;
            }

            // GET: Comments
            public async Task<IActionResult> Index()
            {
                var toiletAppContext = _context.Comments.Include(c => c.Toilets);
                return View(await toiletAppContext.ToListAsync());
            }

            // GET: Comments/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var comments = await _context.Comments
                    .Include(c => c.Toilets)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (comments == null)
                {
                    return NotFound();
                }

                return View(comments);
            }

            // GET: Comments/Create
            public IActionResult Create()
            {
                ViewData["ToiletsId"] = new SelectList(_context.Toilets, "Id", "Id");
                return View();
            }

            // POST: Comments/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,UseDate,Rate,Details,ToiletsId")] Comments comments)
            {

                    _context.Add(comments);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Toilets", new { id = comments.ToiletsId }); 
             }

            // GET: Comments/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var comments = await _context.Comments.FindAsync(id);
                if (comments == null)
                {
                    return NotFound();
                }
                ViewData["ToiletsId"] = new SelectList(_context.Toilets, "Id", "Id", comments.ToiletsId);
                return View(comments);
            }

            // POST: Comments/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,UseDate,Rate,Details,ToiletsId")] Comments comments)
            {
                if (id != comments.Id)
                {
                    return NotFound();
                }

                    try
                    {
                        _context.Update(comments);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CommentsExists(comments.Id))
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

            // GET: Comments/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var comments = await _context.Comments
                    .Include(c => c.Toilets)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (comments == null)
                {
                    return NotFound();
                }

                return View(comments);
            }

            // POST: Comments/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var comments = await _context.Comments.FindAsync(id);
                if (comments != null)
                {
                    _context.Comments.Remove(comments);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool CommentsExists(int id)
            {
                return _context.Comments.Any(e => e.Id == id);
            }
        }
    }
