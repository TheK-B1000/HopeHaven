namespace Collaborative_Resource_Management_System.Models.Interfaces
{
    public interface IEmailService
    {
        Task SendLowStockAlert(IEnumerable<InventoryItem> lowStockItems);
        Task SendTestEmailAsync();
    }


}
