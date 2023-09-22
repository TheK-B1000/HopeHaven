using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class Consumable
    {
        public int ItemID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public decimal PricePer { get; set; }

        public int QuantityAvailable { get; set; }

        public int MinimumQuantity { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public int RoomID { get; set; }
    }
}
