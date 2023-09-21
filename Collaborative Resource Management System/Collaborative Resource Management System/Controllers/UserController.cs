using Microsoft.AspNetCore.Mvc;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Manage()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
    }
}
