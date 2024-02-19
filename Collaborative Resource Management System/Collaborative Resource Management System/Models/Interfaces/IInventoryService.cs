using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IInventoryService
{
    Task<IEnumerable<InventoryItem>> SearchInventoryAsync(string searchString);
    Task<bool> AddCategoryAsync(Category category);
    Task<bool> AddItemAsync(InventoryItem item);
    Task<IEnumerable<Department>> GetDepartmentsAsync();
    Task<InventoryItem> GetItemDetailsAsync(int id);
    Task<IEnumerable<InventoryItem>> GetItemsByTypeAsync(ItemType itemType);
    Task<IEnumerable<SelectListItem>> GetCategoriesAsync();
    Task<bool> EditItemAsync(InventoryItem item); 
    Task<bool> SoftDeleteItemAsync(int id);
    Task<IEnumerable<InventoryItem>> GetLowStockItemsAsync();
    Task<IEnumerable<InventoryItem>> SearchInventoryAsync(string searchString, bool includeInactive);

}

