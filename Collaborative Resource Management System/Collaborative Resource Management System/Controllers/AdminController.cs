using Microsoft.AspNetCore.Mvc;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminIndex(string userName)
        {
            ViewBag.UserName = userName;
            return View();
        }
    }
}
