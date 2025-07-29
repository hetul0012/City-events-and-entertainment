using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using City_events_and_entertainment.Data;
using City_events_and_entertainment.Models;

namespace City_events_and_entertainment.Controllers
{
    public class MuseumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MuseumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Museums
        public async Task<IActionResult> Index()
        {
            var museums = await _context.Museums.Include(m => m.Team).ToListAsync();
            return View(museums);
        }

        // GET: Museums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var museum = await _context.Museums
                .Include(m => m.Team)
                .Include(m => m.MuseumFacilities)
                    .ThenInclude(mf => mf.Facility)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (museum == null) return NotFound();

            return View(museum);
        }

        // GET: Museums/Create
        public IActionResult Create()
        {
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }

        // POST: Museums/Create
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

        // GET: Museums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var museum = await _context.Museums.FindAsync(id);
            if (museum == null) return NotFound();

            ViewBag.Teams = _context.Teams.ToList();
            return View(museum);
        }

        // POST: Museums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Museum museum)
        {
            if (id != museum.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(museum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Museums.Any(e => e.Id == museum.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(museum);
        }

        // GET: Museums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var museum = await _context.Museums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (museum == null) return NotFound();

            return View(museum);
        }

        // POST: Museums/Delete/5
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
