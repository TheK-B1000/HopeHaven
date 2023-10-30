using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext context;
        //TODO - Put a default user in the config file
        string loggedInUserName = "Stella Johnson";

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

            ViewBag.Departments = context.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentID.ToString(),
                Text = d.DeptName
            }).ToList();

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = Request.Form["Name"];
            user.Type = (UserType)Enum.Parse(typeof(UserType), Request.Form["Type"]);
            user.PIN = Request.Form["PIN"];
            user.Password = Request.Form["Password"];
            user.DeptID = int.Parse(Request.Form["DeptID"]);

            try
            {
                context.Update(user);
                context.SaveChanges();
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            try
            {
                department.CreatedDate = DateTime.UtcNow;
                department.EditedDate = DateTime.UtcNow;
                department.CreatedBy = loggedInUserName;
                department.EditedBy = loggedInUserName;

                context.Departments.Add(department);
                await context.SaveChangesAsync();

                return RedirectToAction("Manage");
            }
            catch
            {
                return View("Error", ModelState);
            }
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
            try
            {
                user.CreatedDate = DateTime.UtcNow;
                user.EditedDate = DateTime.UtcNow;
                user.CreatedBy = loggedInUserName;
                user.EditedBy = loggedInUserName;

                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Manage"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
    }
}
