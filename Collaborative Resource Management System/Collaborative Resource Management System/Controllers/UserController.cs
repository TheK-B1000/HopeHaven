using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext context;

        public UserController(AppDbContext dbContext)
        {
            context = dbContext;
        }

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
