
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using City_events_and_entertainment.Data;
using City_events_and_entertainment.Models;

namespace City_events_and_entertainment.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Feedbacks
        public async Task<IActionResult> Index()
        {
            var feedbacks = _context.Feedbacks.Include(f => f.Museum);
            return View(await feedbacks.ToListAsync());
        }

        // GET: Feedbacks/Create
        public IActionResult Create()
        {
            ViewData["MuseumId"] = new SelectList(_context.Museums, "Id", "Name");
            return View();
        }

        // POST: Feedbacks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Comment,Rating,MuseumId")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MuseumId"] = new SelectList(_context.Museums, "Id", "Name", feedback.MuseumId);
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return NotFound();
            ViewData["MuseumId"] = new SelectList(_context.Museums, "Id", "Name", feedback.MuseumId);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,Rating,MuseumId")] Feedback feedback)
        {
            if (id != feedback.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(feedback.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MuseumId"] = new SelectList(_context.Museums, "Id", "Name", feedback.MuseumId);
            return View(feedback);
        }

        // GET: Feedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var feedback = await _context.Feedbacks
                .Include(f => f.Museum)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null) return NotFound();
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var feedback = await _context.Feedbacks
                .Include(f => f.Museum)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedback == null) return NotFound();
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }
    }
}
