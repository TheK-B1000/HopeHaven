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