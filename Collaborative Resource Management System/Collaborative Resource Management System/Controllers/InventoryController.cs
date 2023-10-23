using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Manage()
        {
            var consumables = await context.Consumables.ToListAsync();
            var nonConsumables = await context.NonConsumables.ToListAsync();
            var allItems = consumables.Cast<InventoryItem>().Concat(nonConsumables.Cast<InventoryItem>()).ToList();

            return View(allItems);
        }

        public IActionResult Add()
        {
            ViewBag.Categories = context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.CategoryName
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddConsumable(Consumable consumable)
        {
            try
            {
                consumable.CreatedDate = DateTime.UtcNow;
                consumable.EditedDate = DateTime.UtcNow;
                string loggedInUserName = "Stella Johnson";
                consumable.CreatedBy = loggedInUserName;
                consumable.EditedBy = loggedInUserName;

                context.Consumables.Add(consumable);
                await context.SaveChangesAsync();
                return RedirectToAction("Manage");
            }
            catch
            {
                return View("Error", ModelState);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNonConsumable(NonConsumable nonConsumable)
        {
            try
            {
                nonConsumable.CreatedDate = DateTime.UtcNow;
                nonConsumable.EditedDate = DateTime.UtcNow;
                string loggedInUserName = "Stella Johnson";
                nonConsumable.CreatedBy = loggedInUserName;
                nonConsumable.EditedBy = loggedInUserName;

                context.NonConsumables.Add(nonConsumable);
                await context.SaveChangesAsync();
                return RedirectToAction("Manage");
            }
            catch
            {
                return View("Error", ModelState);
            }
        }


        [HttpGet]
        public IActionResult LoadItemType(string itemType)
        {
            ViewBag.Categories = context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.CategoryName
            }).ToList();

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


        [HttpGet]
        public async Task<IActionResult> Edit(int? id, ItemType type)
        {
            if (id == null)
            {
                return NotFound();
            }

            InventoryItem item;

            if (type == ItemType.Consumable)
            {
                item = await context.Consumables.FindAsync(id);
            }
            else
            {
                item = await context.NonConsumables.FindAsync(id);
            }

            if (item == null)
            {
                return NotFound();
            }


            ViewBag.Categories = context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.CategoryName
            }).ToList();

            return View("Edit", item);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, ItemType type)
        {
            if (id == null)
            {
                return NotFound();
            }

            InventoryItem item;

            if (type == ItemType.Consumable)
            {
                item = await context.Consumables.FindAsync(id);
            }
            else
            {
                item = await context.NonConsumables.FindAsync(id);
            }

            if (item == null)
            {
                return NotFound();
            }

            item.Name = Request.Form["Name"];
            item.Description = Request.Form["Description"];
            item.RoomNumber = int.Parse(Request.Form["RoomNumber"]);
            item.CategoryID = int.Parse(Request.Form["CategoryID"]);
            item.GeneralLedger = Request.Form["GeneralLedger"];
            item.Comments = Request.Form["Comments"];

            try
            {
                if (type == ItemType.Consumable)
                {
                    var consumable = item as Consumable;
                    if (consumable != null)
                    {
                        consumable.PricePerUnit = float.Parse(Request.Form["PricePerUnit"]);
                        consumable.QuantityAvailable = int.Parse(Request.Form["QuantityAvailable"]);
                        consumable.MinimumQuantity = int.Parse(Request.Form["MinimumQuantity"]);
                    }
                }
                else
                {
                    var nonConsumable = item as NonConsumable;
                    if (nonConsumable != null)
                    {
                        nonConsumable.AssetTag = Request.Form["AssetTag"];
                    }
                }

                context.Update(item);
                await context.SaveChangesAsync();
                return RedirectToAction("Manage");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        private bool ItemExists(int id)
        {
            return context.InventoryItems.Any(e => e.InventoryItemID == id);
        }
        public async Task<IActionResult> ConsumableItems()
        {
            var consumables = await context.Consumables.ToListAsync();
            return View(consumables);
        }

        public async Task<IActionResult> NonConsumableItems()
        {
            var nonConsumables = await context.NonConsumables.ToListAsync();
            return View(nonConsumables);
        }

        public async Task<IActionResult> ConsumableDetails(int? id)
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


        public async Task<IActionResult> NonConsumableDetails(int? id)
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