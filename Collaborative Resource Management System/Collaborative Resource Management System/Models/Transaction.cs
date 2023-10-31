using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public int DepartmentID { get; set; }
        public int UserID { get; set; }
    }
}
