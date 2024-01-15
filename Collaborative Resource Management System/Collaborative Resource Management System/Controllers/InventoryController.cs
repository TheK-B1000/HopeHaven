using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Threading.Tasks;

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

        public IActionResult CheckIn()
        {
            return View();
        }
        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LoadItemType(string itemType)
        {
            ViewBag.Categories = await _inventoryService.GetCategoriesAsync();

            if (itemType == "Consumable")
            {
                return PartialView("Consumable");
            }
            else if (itemType == "NonConsumable")
            {
                return PartialView("NonConsumable");
            }
            return NotFound();
        }

        public async Task<IActionResult> ConsumableItems()
        {
            var consumables = await _inventoryService.ConsumableItems();
            return View(consumables);
        }

        public async Task<IActionResult> NonConsumableItems()
        {
            var nonConsumables = await _inventoryService.NonConsumableItems();
            return View(nonConsumables);
        }

        public async Task<IActionResult> ConsumableDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumable = await _inventoryService.ConsumableDetails(id);

            if (consumable == null)
            {
                return NotFound();
            }

            return View(consumable);
        }

        public async Task<IActionResult> NonConsumableDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nonConsumable = await _inventoryService.NonConsumableDetails(id);

            if (nonConsumable == null)
            {
                return NotFound();
            }

            return View(nonConsumable);
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = await _inventoryService.GetCategoriesAsync();
            return View();
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            bool success = await _inventoryService.AddCategoryAsync(category);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddConsumable(Consumable consumable)
        {
            if (consumable.ImageFile != null && consumable.ImageFile.Length > 0)
            {
                var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(consumable.ImageFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await consumable.ImageFile.CopyToAsync(fileStream);
                }

                consumable.Image = fileName; 
            }
            bool success = await _inventoryService.AddConsumableAsync(consumable);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNonConsumable(NonConsumable nonConsumable, IFormFile Image)
        {
            if (Image != null && Image.Length > 0)
            {
                var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(fileStream);
                }

                nonConsumable.Image = fileName;
            }
            bool success = await _inventoryService.AddNonConsumableAsync(nonConsumable);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
        }
        public async Task<IActionResult> Edit(InventoryItem item, IFormFile? ImageFile, ItemType type)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                item.Image = fileName; 
            }

            bool success = await _inventoryService.EditItemAsync(item, type);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }

            ViewBag.Categories = await _inventoryService.GetCategoriesAsync();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InventoryItem item, ItemType type)
        {
            bool success = await _inventoryService.EditItemAsync(item, type);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> SoftDelete(int id, ItemType type)
        {
            var success = await _inventoryService.SoftDeleteItemAsync(id, type);
            if (success)
            {
                return RedirectToAction("Manage");
            }
            else
            {
                return View("Error");
            }
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

