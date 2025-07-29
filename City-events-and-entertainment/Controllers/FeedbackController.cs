using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using City_events_and_entertainment.Data;
using City_events_and_entertainment.Models;

namespace City_events_and_entertainment.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var feedbacks = _context.Feedbacks.Include(f => f.Museum);
            return View(await feedbacks.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var feedback = await _context.Feedbacks
                .Include(f => f.Museum)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (feedback == null) return NotFound();

            return View(feedback);
        }

        public IActionResult Create()
        {
            ViewBag.Museums = _context.Museums.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return NotFound();

            ViewBag.Museums = _context.Museums.ToList();
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Feedback feedback)
        {
            if (id != feedback.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var feedback = await _context.Feedbacks
                .Include(f => f.Museum)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (feedback == null) return NotFound();

            return View(feedback);
        }

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
    }
}
