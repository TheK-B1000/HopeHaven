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
            
            return View();
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
           
            return View();
        }

        public IActionResult NonConsumableItems()
        {
            
            return View();
        }

       
        
           
        public IActionResult NonConsumableDetails(int id)
        {
           

            return View();
        }

        public IActionResult ConsumableDetails(int id)
        {
            

            return View();
        }


    }
}