using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [StringLength(500)]
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string DisplayImageUrl
        {
            get
            {
                return string.IsNullOrWhiteSpace(Image) ? "/img/NotFound.jpg" : Image;
            }
        }

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
        public ItemType ItemType { get; set; }

        // Consumable items
        public float? PricePerUnit { get; set; }
        public int? QuantityAvailable { get; set; }
        public int? MinimumQuantity { get; set; }

        // NonConsumable items
        public string? AssetTag { get; set; }

        [StringLength(500)]
        public string? Comments { get; set; }
        public bool IsActive { get; set; }
    }
}
