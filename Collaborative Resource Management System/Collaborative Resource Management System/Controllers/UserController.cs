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
        public IActionResult Edit(int userId)
        {
            var user = context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(int userId, User updatedUser)
        {
            if (ModelState.IsValid)
            {
                var user = context.Users.Find(userId);
                if (user == null)
                {
                    return NotFound();
                }

                user.Name = updatedUser.Name;
                user.Type = updatedUser.Type;
                user.PIN = updatedUser.PIN;
                user.Password = updatedUser.Password;
                user.DeptID = updatedUser.DeptID;
                user.CreatedBy = updatedUser.CreatedBy;
                user.EditedBy = updatedUser.EditedBy;
                user.CreatedDate = updatedUser.CreatedDate;
                user.EditedDate = updatedUser.EditedDate;
                user.Active = updatedUser.Active;

                context.SaveChanges();

                return RedirectToAction("Manage");
            }

            return View(updatedUser);
        }

        [HttpPost]
        public IActionResult Add(User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);  
                context.SaveChanges(); 

                return RedirectToAction("Manage");
            }

            return View(user);
        }
    }
}
