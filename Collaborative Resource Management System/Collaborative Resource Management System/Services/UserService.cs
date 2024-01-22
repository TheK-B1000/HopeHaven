using Collaborative_Resource_Management_System.Models;
using Collaborative_Resource_Management_System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly string _loggedInUserName = "Stella Johnson";

    public UserService(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<SelectListItem>> GetRolesAsync()
    {
        var roles = await _context.Roles.ToListAsync();
        var roleSelectList = roles.Select(r => new SelectListItem
        {
            Value = r.Name, 
            Text = r.Name
        });
        return roleSelectList;
    }


    public async Task<IEnumerable<IdentityUser>> SearchUsersAsync(string searchString)
    {
        var users = _context.Users.AsQueryable();

        users = users.Where(u => !u.LockoutEnabled || u.LockoutEnd <= DateTimeOffset.Now);

        if (!string.IsNullOrEmpty(searchString))
        {
            if (Guid.TryParse(searchString, out Guid searchId))
            {
                users = users.Where(u => u.Id == searchId.ToString());
            }
            else
            {
                users = users.Where(u => EF.Functions.Like(u.UserName, $"%{searchString}%"));
            }
        }

        return await users.ToListAsync();
    }
    public async Task<IdentityUser> GetUserByIdAsync(string userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<IEnumerable<UserRoleViewModel>> GetUsersWithRolesAsync(string searchString = null, bool includeInactive = false)
    {
        var usersWithRoles = new List<UserRoleViewModel>();
        var usersQuery = _context.Users.AsQueryable();
        if (!includeInactive)
        {
            usersQuery = usersQuery.Where(u => !u.LockoutEnabled || u.LockoutEnd <= DateTimeOffset.Now);
        }

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            usersQuery = usersQuery.Where(u => EF.Functions.Like(u.UserName, $"%{searchString}%"));
        }

        var userRoles = from user in usersQuery
                        join userRole in _context.UserRoles on user.Id equals userRole.UserId
                        join role in _context.Roles on userRole.RoleId equals role.Id
                        select new UserRoleViewModel { User = user, Role = role.Name };

        return await userRoles.ToListAsync();
    }

    public async Task<string> GetRoleForUserAsync(IdentityUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles.FirstOrDefault();
    }

    public async Task<IEnumerable<SelectListItem>> GetDepartmentsAsync()
    {
        return await _context.Departments
            .Select(d => new SelectListItem
            {
                Value = d.DepartmentID.ToString(),
                Text = d.Name
            }).ToListAsync();
    }

    public async Task<bool> EditUserAsync(IdentityUser user) 
    {
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
    public async Task<bool> AddDepartmentAsync(Department department)
    {
        if (department == null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(department.Name) || department.Name.Length > 100)
        {
            return false;
        }

        try
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> AddUserAsync(IdentityUser user, string password, string roleName)
    {
        if (user == null || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(roleName))
        {
            return false; 
        }

        if (!IsValidPassword(password))
        {
            return false;
        }

        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return true;
        }

        return false;
    }

    private bool IsValidPassword(string password)
    {
        return password.Length >= 8; 
    }


    public async Task<bool> MarkUserAsInactiveAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.MaxValue; 

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
    public async Task<bool> ReactivateUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.LockoutEnd = null; 

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

}
