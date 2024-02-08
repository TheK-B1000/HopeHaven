using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; 

namespace Collaborative_Resource_Management_System.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public InventoryController(IInventoryService inventoryService, IWebHostEnvironment hostingEnvironment)
        {
            _inventoryService = inventoryService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Manage(string searchString)
        {
            var allItems = await _inventoryService.SearchInventoryAsync(searchString);
            ViewBag.SearchString = searchString;
            return View(allItems);
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = await _inventoryService.GetCategoriesAsync();
            return View(new InventoryItem()); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(InventoryItem item, IFormFile visibleImage)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _inventoryService.GetCategoriesAsync();
                return View(item);
            }

            if (visibleImage != null && visibleImage.Length > 0)
            {
                var imagesPath = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(visibleImage.FileName);
                var filePath = Path.Combine(imagesPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await visibleImage.CopyToAsync(fileStream);
                }

                item.Image = fileName;
            }

            bool success = await _inventoryService.AddItemAsync(item);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _inventoryService.GetItemByIdAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _inventoryService.GetCategoriesAsync();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InventoryItem item)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _inventoryService.GetCategoriesAsync();
                return View(item);
            }

            bool success = await _inventoryService.EditItemAsync(item);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> SoftDelete(int id)
        {
            var success = await _inventoryService.SoftDeleteItemAsync(id);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
