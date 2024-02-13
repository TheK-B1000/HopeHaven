using Collaborative_Resource_Management_System.Data;
using Collaborative_Resource_Management_System.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Humanizer.In;

namespace Collaborative_Resource_Management_System.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;
        private readonly string _loggedInUserName = "Stella Johnson";
        private readonly bool _isActive = true;
        private readonly bool _isDeleted = false;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryItem>> SearchInventoryAsync(string searchString)
        {
            return await _context.InventoryItems
                .Where(item => item.IsActive &&
                               (string.IsNullOrEmpty(searchString) || EF.Functions.Like(item.Name, $"%{searchString}%")))
                .Include(item => item.Consumable)
                .Include(item => item.NonConsumable)
                .ToListAsync();
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

        public async Task<bool> AddItemAsync(InventoryItem item)
        {
            try
            {
                item.CreatedDate = DateTime.UtcNow;
                item.EditedDate = DateTime.UtcNow;
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

        public async Task<IEnumerable<InventoryItem>> GetItemsByTypeAsync(ItemType itemType)
        {
            return await _context.InventoryItems
                        .Where(item => item.ItemType == itemType)
                        .ToListAsync();
        }

        public async Task<InventoryItem> GetItemDetailsAsync(int id)
        {
            return await _context.InventoryItems
                        .Include(item => item.Consumable) 
                        .Include(item => item.NonConsumable) 
                        .FirstOrDefaultAsync(item => item.InventoryItemID == id);
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
                var item = await _context.InventoryItems
                    .Include(i => i.Consumable)
                    .Include(i => i.NonConsumable)
                    .FirstOrDefaultAsync(i => i.InventoryItemID == updatedItem.InventoryItemID);

                if (item == null) return false;

                _context.Entry(item).CurrentValues.SetValues(updatedItem);

                if (item.ItemType == ItemType.Consumable && item.Consumable != null && updatedItem.Consumable != null)
                {
                    item.Consumable.PricePerUnit = updatedItem.Consumable.PricePerUnit;
                    item.Consumable.QuantityAvailable = updatedItem.Consumable.QuantityAvailable;
                    item.Consumable.MinimumQuantity = updatedItem.Consumable.MinimumQuantity;
                }

                if (item.ItemType == ItemType.NonConsumable && item.NonConsumable != null && updatedItem.NonConsumable != null)
                {
                    item.NonConsumable.AssetTag = updatedItem.NonConsumable.AssetTag;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
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

                item.IsActive = _isDeleted;
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


        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }
    }
}
