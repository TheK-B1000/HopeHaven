using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IInventoryService
{
    Task<List<InventoryItem>> GetActiveItemsByType(ItemType itemType); 
    Task<IEnumerable<InventoryItem>> SearchInventoryAsync(string searchString);
    Task<bool> AddCategoryAsync(Category category);
    Task<IEnumerable<Department>> GetDepartmentsAsync();
    Task<IEnumerable<SelectListItem>> GetCategoriesAsync();
    Task<bool> AddItemAsync(InventoryItem item); 
    Task<bool> EditItemAsync(InventoryItem updatedItem);
    Task<bool> SoftDeleteItemAsync(int id);
    Task<List<InventoryItem>> ConsumableItems();
    Task<List<InventoryItem>> NonConsumableItems();
    Task<InventoryItem> ConsumableDetails(int? id);
    Task<InventoryItem> NonConsumableDetails(int? id);
    Task<InventoryItem> GetItemByIdAsync(int id);


}
