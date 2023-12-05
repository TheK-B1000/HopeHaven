using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Manage(string searchString)
        {
            var users = await _userService.SearchUsersAsync(searchString);
            var userRoleViewModels = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var role = await _userService.GetRoleForUserAsync(user);
                userRoleViewModels.Add(new UserRoleViewModel
                {
                    User = user,
                    Role = role
                });
            }

            ViewBag.SearchString = searchString;
            return View(userRoleViewModels);
        }




        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Departments = await _userService.GetDepartmentsAsync();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityUser user)
        {
            var success = await _userService.EditUserAsync(user);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
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
            var success = await _userService.AddDepartmentAsync(department);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Departments = await _userService.GetDepartmentsAsync();
            ViewBag.Roles = await _userService.GetRolesAsync();
            return View(new IdentityUser());
        }

        [HttpPost]
        public async Task<IActionResult> Add(IdentityUser user, string password, string selectedRole) 
        {
            var success = await _userService.AddUserAsync(user, password, selectedRole); 
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> SoftDelete(string id)
        {
            var success = await _userService.MarkUserAsInactiveAsync(id);
            if (success)
            {
                TempData["Message"] = "User successfully marked as inactive.";
                return RedirectToAction("Manage");
            }
            else
            {
                TempData["Error"] = "Error occurred while marking the user as inactive.";
                return RedirectToAction("Manage");
            }
        }
    }
}
