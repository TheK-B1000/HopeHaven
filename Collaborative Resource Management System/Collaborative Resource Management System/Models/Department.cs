namespace Collaborative_Resource_Management_System.Models
{
    public class Department
    {
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public int CreatedBy { get; set; }
        public int EditedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public bool Active { get; set; }
    }
}
