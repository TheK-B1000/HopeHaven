using Microsoft.AspNetCore.Mvc;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
