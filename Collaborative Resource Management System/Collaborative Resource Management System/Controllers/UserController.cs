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
            bool includeInactive = false;

            var userRoleViewModels = await _userService.GetUsersWithRolesAsync(searchString, includeInactive);
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

            var currentRoles = await _userManager.GetRolesAsync(user);
            var userRole = currentRoles.FirstOrDefault(); 

            ViewBag.UserRole = userRole; 
            ViewBag.AllRoles = await _roleManager.Roles.ToListAsync(); 
            return View(user);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(IdentityUser user, string selectedRole, string password)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;

            var updateResult = await _userManager.UpdateAsync(existingUser);
            var currentRoles = await _userManager.GetRolesAsync(existingUser);
            if (!currentRoles.Contains(selectedRole))
            {
                await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
                await _userManager.AddToRoleAsync(existingUser, selectedRole);
            }

            if (!string.IsNullOrEmpty(password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
                var resetResult = await _userManager.ResetPasswordAsync(existingUser, token, password);
            }

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
                user.LockoutEnabled = false;
                await _userManager.UpdateAsync(user);

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
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.MaxValue;  

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["Message"] = "User successfully marked as inactive.";
                }
                else
                {
                    TempData["Error"] = "Error occurred while marking the user as inactive.";
                }
            }
            else
            {
                TempData["Error"] = "User not found.";
            }

            return RedirectToAction("Manage");
        }


    }
}
