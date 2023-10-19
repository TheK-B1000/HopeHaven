using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var users = context.Users.ToList();
            return View(users);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = context.Users.Find(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Manage");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Departments = context.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentID.ToString(),
                Text = d.DeptName
            }).ToList();
            return View(new User());
        }


        [HttpPost]
        public IActionResult Add(User user)
        {
                user.CreatedDate = DateTime.UtcNow;
                user.EditedDate = DateTime.UtcNow;
                string loggedInUserName = "Stella";
                user.CreatedBy = loggedInUserName;
                user.EditedBy = loggedInUserName;

                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return RedirectToAction("Manage");

            return View(user);
        }

    }
}
