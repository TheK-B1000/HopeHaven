using Microsoft.AspNetCore.Identity;

namespace Collaborative_Resource_Management_System.Models.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityUser> FindByPinAsync(string pin);
    }
}
