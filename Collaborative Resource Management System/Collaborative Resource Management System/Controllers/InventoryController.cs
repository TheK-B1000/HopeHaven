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
            var item = await context.InventoryItems
                .FirstOrDefaultAsync(i => i.InventoryItemID == id);

            if (item == null)
                return NotFound();

            if (item.ItemType == ItemType.Consumable)
            {
                var consumable = await context.Consumables
                    .FirstOrDefaultAsync(c => c.ItemID == id);
                if (consumable == null)
                    return NotFound();

                return View("EditConsumable", consumable);
            }
            else if (item.ItemType == ItemType.NonConsumable)
            {
                var nonConsumable = await context.NonConsumables
                    .FirstOrDefaultAsync(nc => nc.ItemID == id);
                if (nonConsumable == null)
                    return NotFound();

                return View("EditNonConsumable", nonConsumable);
            }

            return BadRequest("Unknown item type.");
        }


        [HttpPost]
        public async Task<IActionResult> EditConsumable(Consumable consumable)
        {
            if (!ModelState.IsValid)
                return View(consumable);

            context.Update(consumable);
            await context.SaveChangesAsync();

            return RedirectToAction("Manage");
        }

        [HttpPost]
        public async Task<IActionResult> EditNonConsumable(NonConsumable nonConsumable)
        {
            if (!ModelState.IsValid)
                return View(nonConsumable);

            context.Update(nonConsumable);
            await context.SaveChangesAsync();

            return RedirectToAction("Manage");
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
