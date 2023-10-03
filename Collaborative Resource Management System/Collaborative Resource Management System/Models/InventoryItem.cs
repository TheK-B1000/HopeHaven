namespace Collaborative_Resource_Management_System.Models
{
    public class InventoryItem
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int EditedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditedDate { get; set; }
        public int RoomID { get; set; }
        public int CategoryID { get; set; }
        public string GeneralLedger { get; set; }
        public string ItemType { get; set; }
        public string Comments { get; set; }
        public bool Active { get; set; }
    }
}
