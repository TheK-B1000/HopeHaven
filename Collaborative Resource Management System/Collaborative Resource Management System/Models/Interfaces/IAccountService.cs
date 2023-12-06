namespace Collaborative_Resource_Management_System.Models.Interfaces
{
    public interface IAccountService
    {
        Task<bool> AuthenticateUserAsync(string username, string pin);
        Task<string> GetUserRoleFromDatabaseAsync(string username);
    }
}
