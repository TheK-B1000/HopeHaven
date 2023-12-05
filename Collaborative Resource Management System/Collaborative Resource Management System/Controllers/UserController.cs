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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUserService userService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
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
            return RedirectToAction("Manage");
            
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
            return RedirectToAction("Manage");
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
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;

            var passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            var createUserResult = await _userManager.CreateAsync(user);
            if (createUserResult.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, selectedRole);
                if (addToRoleResult.Succeeded)
                {
                    return RedirectToAction("Manage");
                }
            }

            ViewBag.Departments = await _userService.GetDepartmentsAsync();
            ViewBag.Roles = await _userService.GetRolesAsync();
            return View(user);
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
