using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PIN { get; set; }
        public string Password { get; set; }
        public int DeptID { get; set; }
        public int CreatedBy { get; set; }
        public int EditedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public bool Active { get; set; }
    }
}
