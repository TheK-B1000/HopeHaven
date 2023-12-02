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
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly string _loggedInUserName = "Stella Johnson";
    private readonly bool _isActive = true;
    private readonly bool _isDeleted = false;

    public UserService(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
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
            Value = r.Id,
            Text = r.Name
        });
        return roleSelectList;
    }

    public async Task<IEnumerable<User>> SearchUsersAsync(string searchString)
    {
        var users = _context.Users.AsQueryable();

        users = users.Where(u => u.IsActive);


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


    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
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

    public async Task<bool> EditUserAsync(User user)
    {
        try
        {
            user.CreatedDate = DateTime.UtcNow;
            user.EditedDate = DateTime.UtcNow;
            user.CreatedBy = _loggedInUserName;
            user.EditedBy = _loggedInUserName;
            user.IsActive = _isActive;

            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> AddDepartmentAsync(Department department)
    {
        try
        {
            department.CreatedDate = DateTime.UtcNow;
            department.EditedDate = DateTime.UtcNow;
            department.CreatedBy = _loggedInUserName;
            department.EditedBy = _loggedInUserName;
            department.IsActive = _isActive;

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> AddUserAsync(User user, string roleName)
    {
        user.CreatedDate = DateTime.UtcNow;
        user.EditedDate = DateTime.UtcNow;
        user.CreatedBy = _loggedInUserName;
        user.EditedBy = _loggedInUserName;
        user.IsActive = _isActive;

        var result = await _userManager.CreateAsync(user, user.PasswordHash);
        if (result.Succeeded)
        {
            // Check if the role exists and add the user to the role
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            return true;
        }

        return false;
    }

    public async Task<bool> SoftDeleteUserAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            user.IsActive = _isDeleted;
            user.EditedDate = DateTime.UtcNow;
            user.EditedBy = _loggedInUserName;

            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> UpdateUserActiveStatusAsync(int userId, bool isActive)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.IsActive = isActive;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
