using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    Task<IEnumerable<User>> SearchUsersAsync(string searchString);
    Task<User> GetUserByIdAsync(int id);
    Task<IEnumerable<SelectListItem>> GetDepartmentsAsync();
    Task<bool> EditUserAsync(User user);
    Task<bool> AddDepartmentAsync(Department department);
    Task<bool> AddUserAsync(User user);
}
