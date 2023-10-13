namespace Collaborative_Resource_Management_System.Models
{
    public class ConsumableInventoryItems
    {
        public int InventoryItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comments { get; set; }
        public float PricePerUnit { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
