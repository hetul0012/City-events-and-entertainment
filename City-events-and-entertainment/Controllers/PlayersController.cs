using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using City_events_and_entertainment.Data;
using City_events_and_entertainment.Models;

namespace City_events_and_entertainment.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var players = _context.Players.Include(p => p.Team);
            return View(await players.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var player = await _context.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null) return NotFound();

            return View(player);
        }

        public IActionResult Create()
        {
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();

            ViewBag.Teams = _context.Teams.ToList();
            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Player player)
        {
            if (id != player.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var player = await _context.Players.Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (player == null) return NotFound();

            return View(player);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
