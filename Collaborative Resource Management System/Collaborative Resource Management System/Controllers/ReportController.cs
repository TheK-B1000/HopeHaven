using Microsoft.AspNetCore.Mvc;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Confirmation()
        {
            return View();
        }
        public IActionResult Report()
        {
            return View();
        }
    }
}
