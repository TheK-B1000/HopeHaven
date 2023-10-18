using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class InventoryController : Controller
    {
        private readonly AppDbContext context;

        public InventoryController(AppDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult CheckIn()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        public IActionResult Manage()
        {
            return View();
        }
        
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddConsumable(Consumable consumable)
        {
            if (ModelState.IsValid)
            {
                consumable.CreatedDate = DateTime.Now;
                consumable.EditedDate = DateTime.Now;
                consumable.CreatedBy = "Stella";
                consumable.EditedBy = "K-B";

                context.Consumables.Add(consumable);
                await context.SaveChangesAsync();
                return RedirectToAction("Confirmation"); 
            }
            return View(consumable); 
        }

        [HttpPost]
        public async Task<IActionResult> AddNonConsumable(NonConsumable nonConsumable)
        {
            if (ModelState.IsValid)
            {
                nonConsumable.CreatedDate = DateTime.Now;
                nonConsumable.EditedDate = DateTime.Now;
                nonConsumable.CreatedBy = "Stella";
                nonConsumable.EditedBy = "K-B";

                context.NonConsumables.Add(nonConsumable);
                await context.SaveChangesAsync();
                return RedirectToAction("Confirmation"); 
            }
            return View(nonConsumable); 
        }

        [HttpGet]
        public IActionResult LoadItemType(string itemType)
        {
            if (itemType == "Consumable")
            {
                return PartialView("_ConsumableForm");  
            }
            else if (itemType == "NonConsumable")
            {
                return PartialView("_NonConsumableForm");  
            }
            return NotFound();
        }

        public async Task<IActionResult> EditConsumable(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await context.Consumables.FindAsync(id);
            if (consumable == null)
            {
                return NotFound();
            }

            return View(consumable);
        }
        public async Task<IActionResult> EditNonConsumable(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonConsumable = await context.NonConsumables.FindAsync(id);
            if (nonConsumable == null)
            {
                return NotFound();
            }

            return View(nonConsumable);
        }

        [HttpPost]
        public async Task<IActionResult> EditConsumable(int id, Consumable consumable)
        {
            if (id != consumable.InventoryItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(consumable);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumableExists(consumable.InventoryItemID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Manage));
            }
            return View(consumable);
        }

        [HttpPost]
        public async Task<IActionResult> EditNonConsumable(int id, NonConsumable nonConsumable)
        {
            if (id != nonConsumable.InventoryItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(nonConsumable);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonConsumableExists(nonConsumable.InventoryItemID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Manage));
            }
            return View(nonConsumable);
        }
        private bool ConsumableExists(int id)
        {
            return context.Consumables.Any(e => e.InventoryItemID == id);
        }

        private bool NonConsumableExists(int id)
        {
            return context.NonConsumables.Any(e => e.InventoryItemID == id);
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

    }
}