using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using City_events_and_entertainment.Data;
using City_events_and_entertainment.Models;

namespace City_events_and_entertainment.Controllers
{
    public class FacilityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacilityController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Facilities.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var facility = await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
            if (facility == null) return NotFound();

            return View(facility);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Facility facility)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facility);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null) return NotFound();

            return View(facility);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Facility facility)
        {
            if (id != facility.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(facility);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facility);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var facility = await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
            if (facility == null) return NotFound();

            return View(facility);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facility = await _context.Facilities.FindAsync(id);
            if (facility != null)
            {
                _context.Facilities.Remove(facility);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
