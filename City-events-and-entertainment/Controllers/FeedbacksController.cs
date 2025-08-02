using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using City_events_and_entertainment.Data;
using City_events_and_entertainment.Models;
using Microsoft.AspNetCore.Authorization;

namespace City_events_and_entertainment.Controllers
{
    [Authorize]
    public class FeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var feedbacks = await _context.Feedbacks.Include(f => f.Museum).ToListAsync();
            return View(feedbacks);
        }

        public IActionResult Create(int museumId)
        {
            var feedback = new Feedback
            {
                MuseumId = museumId
            };
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feedback);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _context.Feedbacks.Include(f => f.Museum)
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
