using System;
using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public enum ItemType
    {
        Consumable,
        NonConsumable
    }

    public class InventoryItem
    {
        public int InventoryItemID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(100)]
        public string? EditedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? EditedDate { get; set; }

        [Range(1, 500)] 
        public int RoomNumber { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string GeneralLedger { get; set; }

        [Required]
        public ItemType ItemType { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }       

    }
    public class Consumable : InventoryItem
    {
        [Range(0.01, 10000.00)] 
        public float PricePerUnit { get; set; }

        [Range(0, int.MaxValue)]
        public int QuantityAvailable { get; set; }

        [Range(0, int.MaxValue)]
        public int MinimumQuantity { get; set; }
    }

    public class NonConsumable : InventoryItem
    {
        [Required]
        [StringLength(100)]
        public string AssetTag { get; set; }
    }
}