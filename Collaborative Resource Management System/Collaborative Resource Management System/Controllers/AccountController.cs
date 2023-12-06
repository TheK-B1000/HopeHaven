using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                        // Log the user in
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home"); // Or wherever you want to redirect
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid PIN or username.");
            }
            return View(model);
        }
    }
}
