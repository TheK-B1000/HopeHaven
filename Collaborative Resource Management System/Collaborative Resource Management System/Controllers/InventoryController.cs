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

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Consumable consumable)
        {
            if (ModelState.IsValid)
            {
                context.Consumables.Add(consumable);
                context.SaveChanges();
                return View();
            }
            return View(consumable);
        }
        public IActionResult Edit()
        {
            return View();
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
