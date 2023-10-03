namespace Collaborative_Resource_Management_System.Models
{
    public enum ItemType
    {
        Consumable,
        NonConsumable
    }

    public class InventoryItem
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string EditedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public int RoomNumber { get; set; }
        public int CategoryID { get; set; }
        public string GeneralLedger { get; set; }
        public ItemType ItemType { get; set; }
        public string Comments { get; set; }
        public bool Active { get; set; }
    }
}
