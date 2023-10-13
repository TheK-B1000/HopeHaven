using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult LoadItemType(string itemType)
        {
            if (itemType == "Consumable")
            {
                return PartialView("Consumable", new Consumable());
            }
            else if (itemType == "NonConsumable")
            {
                return PartialView("NonConsumable", new NonConsumable());
            }
            return BadRequest("Invalid item type.");
        }

        [HttpPost]
        public IActionResult AddConsumable(Consumable consumable)
        {
            if (ModelState.IsValid)
            {
                context.Consumables.Add(consumable);

                context.SaveChanges();

                return RedirectToAction("Manage");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddNonConsumable(NonConsumable nonConsumable)
        {
            if (ModelState.IsValid)
            {
                context.NonConsumables.Add(nonConsumable);

                context.SaveChanges();

                return RedirectToAction("Manage");
            }
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var inventoryItem = await context.InventoryItems
                .FirstOrDefaultAsync(i => i.InventoryItemID == id);

            if (inventoryItem == null)
                return NotFound();

            InventoryEditType viewModel = new InventoryEditType
            {
                InventoryItem = inventoryItem
            };

            if (inventoryItem.ItemType == ItemType.Consumable)
            {
                var consumable = await context.Consumables
                    .FirstOrDefaultAsync(c => c.ConsumableID == id); 
                if (consumable == null)
                    return NotFound();

                viewModel.Consumable = consumable;
            }
            else if (inventoryItem.ItemType == ItemType.NonConsumable)
            {
                var nonConsumable = await context.NonConsumables
                    .FirstOrDefaultAsync(nc => nc.NonConsumableID == id);  
                if (nonConsumable == null)
                    return NotFound();

                viewModel.NonConsumable = nonConsumable;
            }

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(InventoryEditType viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            context.Update(viewModel.InventoryItem);
            if (viewModel.InventoryItem.ItemType == ItemType.Consumable)
                context.Update(viewModel.Consumable);
            else if (viewModel.InventoryItem.ItemType == ItemType.NonConsumable)
                context.Update(viewModel.NonConsumable);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Manage));
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult ConsumableItems()
        {
            var consumables = context.Consumables.ToList();
            return View(consumables);
        }

        public IActionResult NonConsumableItems()
        {
            var nonConsumables = context.NonConsumables.ToList();
            return View(nonConsumables);
        }

    }
}