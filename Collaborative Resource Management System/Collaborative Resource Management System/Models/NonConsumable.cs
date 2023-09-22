using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class NonConsumable
    {
        public int ItemID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string AssetTag { get; set; }

        public string GeneralLedger { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public int RoomID { get; set; }

    }
}
