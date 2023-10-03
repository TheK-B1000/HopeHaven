using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class CheckIn
    {
        public int CheckInID { get; set; }
        public string AssetTag { get; set; }
        public DateTime CheckInDate { get; set; }
        public int UserID { get; set; }

    }
}
