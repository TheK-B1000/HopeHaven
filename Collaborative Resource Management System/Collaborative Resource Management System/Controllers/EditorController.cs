using Microsoft.AspNetCore.Mvc;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class EditorController : Controller
    {
        public IActionResult EditorIndex()
        {
            return View();
        }
    }
}
