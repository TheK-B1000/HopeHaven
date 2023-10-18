namespace Collaborative_Resource_Management_System.Models
{
    public class InventoryIntake
    {
        public int InventoryIntakeID { get; set; }
        public int InventoryItemID { get; set; }
        public DateTime IntakeDate { get; set; }
        public int Quantity { get; set; }
        public float PurchasePrice { get; set; }
    }
}
