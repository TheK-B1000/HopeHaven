using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IdentityUser> FindByPinAsync(string pin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordHash == pin);

            return user;
        }

    }
}
