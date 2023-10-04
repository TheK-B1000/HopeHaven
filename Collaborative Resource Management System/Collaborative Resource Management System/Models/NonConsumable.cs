using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class NonConsumable
    {
        public int NonConsumableID { get; set; }
        public string AssetTag { get; set; }
        public int ItemID { get; set; }

    }
}
