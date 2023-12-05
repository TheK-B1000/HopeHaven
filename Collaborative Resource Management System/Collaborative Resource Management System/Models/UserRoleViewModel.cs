using Microsoft.AspNetCore.Identity;

namespace Collaborative_Resource_Management_System.Models
{
    public class UserRoleViewModel
    {
        public IdentityUser User { get; set; }
        public string Role { get; set; }
    }
}