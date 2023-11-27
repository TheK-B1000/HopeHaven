using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IInventoryService
{
    Task<IEnumerable<InventoryItem>> SearchInventoryAsync(string searchString);
    Task<bool> AddCategoryAsync(Category category);
    Task<bool> AddConsumableAsync(Consumable consumable);
    Task<bool> AddNonConsumableAsync(NonConsumable nonConsumable);
    Task<InventoryItem> GetItemByIdAsync(int id, ItemType type);
    Task<IEnumerable<SelectListItem>> GetCategoriesAsync();
    Task<bool> EditItemAsync(InventoryItem item, ItemType type);
    Task<List<Consumable>> ConsumableItems();
    Task<List<NonConsumable>> NonConsumableItems();
    Task<Consumable> ConsumableDetails(int? id);
    Task<NonConsumable> NonConsumableDetails(int? id);
    Task<bool> SoftDeleteItemAsync(int id, ItemType type);
}
