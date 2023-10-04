namespace Collaborative_Resource_Management_System.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DeptName { get; set; }
        public string CreatedBy { get; set; }
        public string EditedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public bool Active { get; set; }
    }
}
