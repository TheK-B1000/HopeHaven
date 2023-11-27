﻿using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly string _loggedInUserName = "Stella Johnson";
    private readonly bool _isActive = true;
    private readonly bool _isDeleted = false;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> SearchUsersAsync(string searchString)
    {
        var users = _context.Users.AsQueryable();

        users = users.Where(u => u.IsActive);

        if (!string.IsNullOrEmpty(searchString))
        {
            bool isNumeric = int.TryParse(searchString, out int searchNumber);

            if (isNumeric)
            {
                users = users.Where(u => u.UserID == searchNumber);
            }
            else
            {
                if (Enum.TryParse<UserType>(searchString, true, out var userType))
                {
                    users = users.Where(u => u.Type == userType);
                }
                else
                {
                    users = users.Where(u => EF.Functions.Like(u.Name, $"%{searchString}%"));
                }
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

    public async Task<bool> AddUserAsync(User user)
    {
        try
        {
            user.CreatedDate = DateTime.UtcNow;
            user.EditedDate = DateTime.UtcNow;
            user.CreatedBy = _loggedInUserName;
            user.EditedBy = _loggedInUserName;
            user.IsActive = _isActive;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
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

}
