using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class Consumable
    {
        public int ConsumableID { get; set; }
        public int ItemID { get; set; }
        public float PricePerUnit { get; set; }
        public int QuantityAvailable { get; set; }
        public int MinimumQuantity { get; set; }
    }
}
