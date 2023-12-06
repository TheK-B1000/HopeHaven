using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Collaborative_Resource_Management_System.Data;
using Collaborative_Resource_Management_System.Models.Interfaces; 

namespace Collaborative_Resource_Management_System.Services
{
    public class AccountService : IAccountService 
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<bool> AuthenticateUserAsync(string username, string pin)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                // Use Identity PasswordHasher to verify the PIN
                var passwordVerificationResult = await _userManager.CheckPasswordAsync(user, pin);
                if (passwordVerificationResult)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return true;
                }
            }
            return false;
        }

        public async Task<string> GetUserRoleFromDatabaseAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var userRole = (from ur in _context.UserRoles
                                join r in _context.Roles on ur.RoleId equals r.Id
                                where ur.UserId == user.Id
                                select r.Name).FirstOrDefault();

                return userRole;
            }
            return null;
        }

    }
}
