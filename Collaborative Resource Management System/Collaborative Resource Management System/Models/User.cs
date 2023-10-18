using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public enum UserType
    {
        Admin,
        Editor,
        Staff
    }

    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public UserType Type { get; set; }
        public string PIN { get; set; }
        public string Password { get; set; }
        public int DeptID { get; set; }
        public string CreatedBy { get; set; }
        public string? EditedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public bool Active { get; set; }
    }
}
