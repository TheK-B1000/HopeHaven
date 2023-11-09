using Collaborative_Resource_Management_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Collaborative_Resource_Management_System.Models
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchString)
        {
            var users = _context.Users.AsQueryable();

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

        public async Task UpdateUserAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> GetDepartmentsAsSelectListItems()
        {
            return _context.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentID.ToString(),
                Text = d.DeptName
            }).ToList();
        }
    }

}
