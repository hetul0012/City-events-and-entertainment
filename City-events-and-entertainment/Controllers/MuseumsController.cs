using City_events_and_entertainment.Data;
using City_events_and_entertainment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace City_events_and_entertainment.Controllers
{
    public class MuseumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MuseumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var museums = await _context.Museums
                .Include(m => m.Team)
                .ThenInclude(t => t.Players)
                .Include(m => m.MuseumFacilities)
                .ThenInclude(mf => mf.Facility)
                .Include(m => m.Feedbacks)
                .ToListAsync();

            return View(museums);
        }

        public async Task<IActionResult> Details(int id)
        {
            var museum = await _context.Museums
                .Include(m => m.Team)
                .ThenInclude(t => t.Players)
                .Include(m => m.MuseumFacilities)
                .ThenInclude(mf => mf.Facility)
                .Include(m => m.Feedbacks)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (museum == null) return NotFound();

            return View(museum);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Museum museum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(museum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(museum);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var museum = await _context.Museums.FindAsync(id);
            if (museum == null) return NotFound();
            return View(museum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Museum museum)
        {
            if (id != museum.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(museum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(museum);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var museum = await _context.Museums.FindAsync(id);
            if (museum == null) return NotFound();
            return View(museum);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var museum = await _context.Museums.FindAsync(id);
            if (museum != null)
            {
                _context.Museums.Remove(museum);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
