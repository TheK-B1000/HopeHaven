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

        

        
        
        public IActionResult Add()
        {
            return View();
        }
       
        public async Task<IActionResult> Edit(int id)
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