using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Collaborative_Resource_Management_System.Models
{
    public enum UserType
    {
        Admin,
        Editor,
        Staff
    }

    public class User : IdentityUser
    {
        [Required]
        [StringLength(6)] 
        public string PIN { get; set; }

        public int DeptID { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        public string? EditedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public bool IsActive { get; set; }

    }
}
