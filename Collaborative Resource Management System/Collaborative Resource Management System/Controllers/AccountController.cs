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
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, pin);
                if (result == PasswordVerificationResult.Success)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userRole = roles.FirstOrDefault(); 
                    return Ok(new { isValid = true, username = user.UserName, role = userRole });
                }
            }
            return Ok(new { isValid = false });
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
