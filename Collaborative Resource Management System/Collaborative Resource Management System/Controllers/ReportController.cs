using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ReportController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Report()
        {
            var checkouts = _dbContext.CheckOuts.ToList();
            return View(checkouts);
        }
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
