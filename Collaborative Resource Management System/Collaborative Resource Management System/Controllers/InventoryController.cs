using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var consumables = await _inventoryService.GetItemsByTypeAsync(ItemType.Consumable);
            return View("Items", consumables); 
        }

        public async Task<IActionResult> NonConsumableItems()
        {
            var nonConsumables = await _inventoryService.GetItemsByTypeAsync(ItemType.NonConsumable);
            return View("Items", nonConsumables); 
        }

        public async Task<IActionResult> ConsumableDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _inventoryService.GetItemDetailsAsync(id.Value);
            if (item == null || item.ItemType != ItemType.Consumable)
            {
                return NotFound();
            }

            return View("ItemDetails", item); 
        }

        public async Task<IActionResult> NonConsumableDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _inventoryService.GetItemDetailsAsync(id.Value);
            if (item == null || item.ItemType != ItemType.NonConsumable)
            {
                return NotFound();
            }

            return View("ItemDetails", item);
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
            if (!ModelState.IsValid)
            {
                return View(category);
            }

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
        public async Task<IActionResult> AddItem(InventoryItem item, IFormFile VisibleImage, string itemType)
        {
            if (VisibleImage != null && VisibleImage.Length > 0)
            {
                var imagesPath = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(VisibleImage.FileName);
                var filePath = Path.Combine(imagesPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await VisibleImage.CopyToAsync(fileStream);
                }

                item.Image = "/img/" + fileName;
            }


            if (itemType.Equals("Consumable", StringComparison.OrdinalIgnoreCase))
            {
                item.ItemType = ItemType.Consumable;
                item.Consumable = new Consumable
                {
                    PricePerUnit = Convert.ToSingle(Request.Form["PricePerUnit"]),
                    QuantityAvailable = Convert.ToInt32(Request.Form["QuantityAvailable"]),
                    MinimumQuantity = Convert.ToInt32(Request.Form["MinimumQuantity"])
                };
            }
            else if (itemType.Equals("NonConsumable", StringComparison.OrdinalIgnoreCase))
            {
                item.ItemType = ItemType.NonConsumable;
                item.NonConsumable = new NonConsumable
                {
                    AssetTag = Request.Form["AssetTag"]
                };
            }

            var success = await _inventoryService.AddItemAsync(item);
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

            var item = await _inventoryService.GetItemDetailsAsync(id.Value);
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


        public IActionResult Confirmation()
        {
            return View();
        }

        public async Task<IActionResult> Cart()
        {
            var departments = await _inventoryService.GetDepartmentsAsync(); 
            ViewBag.Departments = departments;
            return View();
        }

    }
}

