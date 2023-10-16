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
            var user = context.Users.Find(2); 
            if (user == null)
            {
                return NotFound();
            }
            return View(user); 
        }


        [HttpPost]
        public IActionResult Edit(User updatedUser, int userId = 2)
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
                user.Active = updatedUser.Active;
                user.EditedBy = "K-B"; 
                user.EditedDate = DateTime.Now;

                context.Entry(user).State = EntityState.Modified;

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
