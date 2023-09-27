using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class CheckIn
    {
        public int RecordID { get; set; }

        public string AssetTag { get; set; }

        public DateTime CheckInDate { get; set; }

        public TimeSpan CheckInTime { get; set; }

        public int CheckedInByUserID { get; set; }
    }
}
