using Collaborative_Resource_Management_System.Data;
using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly bool _isDeleted = false;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Consumable>> ConsumableItems()
        {
            return await _context.Consumables.ToListAsync();
        }

        public async Task<List<NonConsumable>> NonConsumableItems()
        {
            return await _context.NonConsumables.ToListAsync();
        }

        public async Task<Consumable> ConsumableDetails(int? id)
        {
            return id == null ? null : await _context.Consumables.FindAsync(id);
        }

        public async Task<NonConsumable> NonConsumableDetails(int? id)
        {
            return id == null ? null : await _context.NonConsumables.FindAsync(id);
        }

        public async Task<IEnumerable<InventoryItem>> SearchInventoryAsync(string searchString)
        {
            var consumablesList = await _context.Consumables
                .Where(c => c.IsActive && (string.IsNullOrEmpty(searchString) || EF.Functions.Like(c.Name, $"%{searchString}%")))
                .ToListAsync();

            var nonConsumablesList = await _context.NonConsumables
                .Where(nc => nc.IsActive && (string.IsNullOrEmpty(searchString) || EF.Functions.Like(nc.Name, $"%{searchString}%")))
                .ToListAsync();

            var allItems = consumablesList.Cast<InventoryItem>()
                .Concat(nonConsumablesList.Cast<InventoryItem>());

            return allItems;
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

        public async Task<bool> AddConsumableAsync(Consumable consumable)
        {
            try
            {
                consumable.CreatedDate = DateTime.UtcNow;
                consumable.EditedDate = DateTime.UtcNow;
                consumable.CreatedBy = _loggedInUserName;
                consumable.EditedBy = _loggedInUserName;
                consumable.IsActive = _isActive;

                _context.Consumables.Add(consumable);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddNonConsumableAsync(NonConsumable nonConsumable)
        {
            try
            {
                nonConsumable.CreatedDate = DateTime.UtcNow;
                nonConsumable.EditedDate = DateTime.UtcNow;
                nonConsumable.CreatedBy = _loggedInUserName;
                nonConsumable.EditedBy = _loggedInUserName;
                nonConsumable.IsActive = _isActive;

                _context.NonConsumables.Add(nonConsumable);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<InventoryItem> GetItemByIdAsync(int id, ItemType type)
        {
            if (type == ItemType.Consumable)
            {
                return await _context.Consumables.FindAsync(id);
            }
            else
            {
                return await _context.NonConsumables.FindAsync(id);
            }
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

        public async Task<bool> EditItemAsync(InventoryItem updatedItem, ItemType type)
        {
            if (updatedItem == null)
            {
                return false;
            }

            try
            {
                updatedItem.CreatedDate = DateTime.UtcNow;
                updatedItem.EditedDate = DateTime.UtcNow;
                updatedItem.CreatedBy = _loggedInUserName;
                updatedItem.EditedBy = _loggedInUserName;
                updatedItem.IsActive = _isActive;

                if (type == ItemType.Consumable)
                {
                    var item = await _context.Consumables.FindAsync(updatedItem.InventoryItemID);
                    if (item == null) return false;

                    _context.Entry(item).CurrentValues.SetValues(updatedItem);
                }
                else if (type == ItemType.NonConsumable)
                {
                    var item = await _context.NonConsumables.FindAsync(updatedItem.InventoryItemID);
                    if (item == null) return false;

                    _context.Entry(item).CurrentValues.SetValues(updatedItem);
                }
                else
                {
                    return false;
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> SoftDeleteItemAsync(int id, ItemType type)
        {
            try
            {
                InventoryItem item;
                if (type == ItemType.Consumable)
                {
                    item = await _context.Consumables.FindAsync(id);
                }
                else if (type == ItemType.NonConsumable)
                {
                    item = await _context.NonConsumables.FindAsync(id);
                }
                else
                {
                    return false;
                }

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
    }
}
