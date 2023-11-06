using Microsoft.AspNetCore.Mvc;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class EditorController : Controller
    {
        public IActionResult EditorIndex(string userName)
        {
            ViewBag.UserName = userName;
            return View();
        }
    }
}
