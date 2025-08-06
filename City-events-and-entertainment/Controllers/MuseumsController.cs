using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: Museum
        public async Task<IActionResult> Index()
        {
            var museums = _context.Museums.Include(m => m.Team);
            return View(await museums.ToListAsync());
        }

        // GET: Museum/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Museums == null)
                return NotFound();

            var museum = await _context.Museums
                .Include(m => m.Team)
                .Include(m => m.Bookings)
                .Include(m => m.Feedbacks)
                .Include(m => m.MuseumFacilities)
                    .ThenInclude(mf => mf.Facility)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (museum == null)
                return NotFound();

            return View(museum);
        }

        // GET: Museum/Create
        public IActionResult Create()
        {
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: Museum/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,Description,ImageUrl,TeamId")] Museum museum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(museum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", museum.TeamId);
            return View(museum);
        }

        // GET: Museum/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Museums == null)
                return NotFound();

            var museum = await _context.Museums.FindAsync(id);
            if (museum == null)
                return NotFound();

            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", museum.TeamId);
            return View(museum);
        }

        // POST: Museum/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Description,ImageUrl,TeamId")] Museum museum)
        {
            if (id != museum.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(museum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuseumExists(museum.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", museum.TeamId);
            return View(museum);
        }

        // GET: Museum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Museums == null)
                return NotFound();

            var museum = await _context.Museums
                .Include(m => m.Team)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (museum == null)
                return NotFound();

            return View(museum);
        }

        // POST: Museum/Delete/5
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

        private bool MuseumExists(int id)
        {
            return _context.Museums.Any(e => e.Id == id);
        }
    }
}
