using Collaborative_Resource_Management_System.Data;
using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collaborative_Resource_Management_System.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;
        private readonly string _loggedInUserName = "Stella Johnson";
        private readonly bool _isActive = true;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InventoryItem>> GetActiveItemsByType(ItemType itemType)
        {
            return await _context.InventoryItems
                .Where(item => item.IsActive && item.ItemType == itemType)
                .ToListAsync();
        }

        public async Task<List<InventoryItem>> ConsumableItems()
        {
            return await _context.InventoryItems
                                 .Where(item => item.ItemType == ItemType.Consumable)
                                 .ToListAsync();
        }

        public async Task<List<InventoryItem>> NonConsumableItems()
        {
            return await _context.InventoryItems
                                 .Where(item => item.ItemType == ItemType.NonConsumable)
                                 .ToListAsync();
        }

        public async Task<InventoryItem> ConsumableDetails(int? id)
        {
            if (id == null) return null;
            return await _context.InventoryItems
                                 .FirstOrDefaultAsync(item => item.InventoryItemID == id && item.ItemType == ItemType.Consumable);
        }

        public async Task<InventoryItem> NonConsumableDetails(int? id)
        {
            if (id == null) return null;
            return await _context.InventoryItems
                                 .FirstOrDefaultAsync(item => item.InventoryItemID == id && item.ItemType == ItemType.NonConsumable);
        }


        public async Task<IEnumerable<InventoryItem>> SearchInventoryAsync(string searchString)
        {
            return await _context.InventoryItems
                .Where(item => item.IsActive && (string.IsNullOrEmpty(searchString) || EF.Functions.Like(item.Name, $"%{searchString}%")))
                .ToListAsync();
        }

        public async Task<bool> AddItemAsync(InventoryItem item)
        {
            try
            {
                item.CreatedDate = DateTime.UtcNow;
                item.EditedDate = item.CreatedDate;
                item.CreatedBy = _loggedInUserName;
                item.EditedBy = _loggedInUserName;
                item.IsActive = _isActive;

                _context.InventoryItems.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<InventoryItem> GetItemByIdAsync(int id)
        {
            return await _context.InventoryItems.FindAsync(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryID.ToString(),
                    Text = c.Name
                }).ToListAsync();
        }

        public async Task<bool> EditItemAsync(InventoryItem updatedItem)
        {
            if (updatedItem == null)
            {
                return false;
            }
            try
            {
                var item = await _context.InventoryItems.FindAsync(updatedItem.InventoryItemID);
                if (item == null) return false;

                updatedItem.EditedDate = DateTime.UtcNow;
                updatedItem.EditedBy = _loggedInUserName;

                _context.Entry(item).CurrentValues.SetValues(updatedItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SoftDeleteItemAsync(int id)
        {
            try
            {
                var item = await _context.InventoryItems.FindAsync(id);
                if (item == null)
                {
                    return false;
                }

                item.IsActive = false;
                item.EditedDate = DateTime.UtcNow;
                item.EditedBy = _loggedInUserName;

                _context.Update(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            try
            {
                category.CreatedDate = DateTime.UtcNow;
                category.EditedDate = DateTime.UtcNow;
                category.CreatedBy = _loggedInUserName;
                category.EditedBy = _loggedInUserName;
                category.IsActive = _isActive;

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }
    }
}
