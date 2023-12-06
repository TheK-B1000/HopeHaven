using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Collaborative_Resource_Management_System.Services; 

namespace Collaborative_Resource_Management_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AccountService _accountService; 

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService; 
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/validatePin")]
        public async Task<IActionResult> ValidatePin(string pin)
        {
            var userManager = _userManager; 
            var user = await userManager.FindByNameAsync(pin);

            if (user != null)
            {
                var passwordHash = user.PasswordHash;
                return Ok(new { isValid = true, passwordHash });
            }

            return Ok(new { isValid = false });
        }

        [HttpPost]
        public async Task<IActionResult> GetUserRole([FromBody] PinLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userRole = await _accountService.GetUserRoleFromDatabaseAsync(model.Username);

                if (!string.IsNullOrEmpty(userRole))
                {
                    return Json(new { Role = userRole });
                }
            }

            return BadRequest("Error retrieving user role.");
        }

        [HttpPost]
        public async Task<IActionResult> Login(PinLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var passwordHasher = new PasswordHasher<IdentityUser>();
                    var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Pin);
                    if (result == PasswordVerificationResult.Success)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("AdminIndex", "Admin"); 
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid PIN or username.");
            }
            return View(model);
        }
    }
}
