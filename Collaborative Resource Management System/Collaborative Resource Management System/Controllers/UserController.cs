using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
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
            ViewBag.SearchString = searchString;
            return View(users);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Departments = await _userService.GetDepartmentsAsync();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
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
            return View(new User());
        }

        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            var success = await _userService.AddUserAsync(user);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
