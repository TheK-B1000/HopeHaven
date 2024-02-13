using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Collaborative_Resource_Management_System.Services; 

namespace Collaborative_Resource_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("validatePin")]
        public async Task<IActionResult> ValidatePin(string pin)
        {
            try
            {
                var user = await _accountService.FindByPinAsync(pin);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userRole = roles.FirstOrDefault();
                    return Ok(new { isValid = true, username = user.UserName, role = userRole });
                }
                return Ok(new { isValid = false });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Login(string pin)
        {
            return View();
        }

    }
}
