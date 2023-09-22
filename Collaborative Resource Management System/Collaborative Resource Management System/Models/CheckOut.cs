using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class CheckOut
    {
        public int RecordID { get; set; }

        public int UserID { get; set; }

        public int ItemID { get; set; }

        public DateTime CheckOutDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public bool IsPermanent { get; set; }
    }
}
