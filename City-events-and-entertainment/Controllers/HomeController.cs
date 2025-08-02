using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using City_events_and_entertainment.Data;

namespace City_events_and_entertainment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var museums = await _context.Museums
                .Include(m => m.Team)
                .Include(m => m.MuseumFacilities)
                    .ThenInclude(mf => mf.Facility)
                .ToListAsync();

            return View(museums);
        }
    }
}
