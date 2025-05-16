using Microsoft.AspNetCore.Mvc;
using Restoran.DAL;
using Restoran.Models;

namespace Restoran.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Chef> chefs=_context.Chefs.ToList();
            return View(chefs);
        }
    }
}
