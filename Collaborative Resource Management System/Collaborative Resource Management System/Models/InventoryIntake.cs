namespace Collaborative_Resource_Management_System.Models
{
    public class InventoryIntake
    {
        public int IntakeID { get; set; }
        public int ItemID { get; set; }
        public DateTime IntakeDate { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
