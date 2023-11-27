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

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public UserType Type { get; set; }

        [Required]
        [StringLength(6)] 
        public string PIN { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100)]
        public string Password { get; set; }
        public int DeptID { get; set; }

        [Required]
        public string CreatedBy { get; set; }
        public string? EditedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public bool IsActive { get; set; }

    }
}
