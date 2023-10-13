namespace Collaborative_Resource_Management_System.Models
{
    public class InventoryEditType
    {
        public InventoryItem InventoryItem { get; set; }
        public Consumable Consumable { get; set; }
        public NonConsumable NonConsumable { get; set; }
    }
}
