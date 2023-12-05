using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    Task<IEnumerable<IdentityUser>> SearchUsersAsync(string searchString);
    Task<IdentityUser> GetUserByIdAsync(string id);
    Task<IEnumerable<SelectListItem>> GetDepartmentsAsync();
    Task<IEnumerable<SelectListItem>> GetRolesAsync();
    Task<bool> EditUserAsync(IdentityUser user);
    Task<bool> AddDepartmentAsync(Department department);
    Task<bool> AddUserAsync(IdentityUser user, string password, string selectedRole); 
    Task<bool> MarkUserAsInactiveAsync(string userId);
    Task<bool> ReactivateUserAsync(string userId);
    Task<string> GetRoleForUserAsync(IdentityUser user);
}
