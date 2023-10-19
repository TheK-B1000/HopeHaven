using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;



namespace Collaborative_Resource_Management_System.Models
{
    public static class DbInitializer
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServiceProvider = scope.ServiceProvider;
                var context = scopedServiceProvider.GetRequiredService<AppDbContext>();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User
                        {
                            Name = "Beny Farfan",
                            Type = UserType.Admin,
                            PIN = "5678",
                            Password = "securePassword123",
                            DeptID = 2,
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.UtcNow,
                            EditedDate = DateTime.UtcNow,
                        }
                    );
                }

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category
                        {
                            CategoryName = "Electronics",
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.UtcNow,
                            EditedDate = DateTime.UtcNow,
                        }
                    );
                }
              
                context.SaveChanges(); 
              
                if (!context.InventoryItems.OfType<Consumable>().Any())
                {
                    context.InventoryItems.AddRange(
                        new Consumable
                        {
                            Name = "Glue Sticks",
                            Description = "We love glue sticks",
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.UtcNow,
                            EditedDate = DateTime.UtcNow,
                            RoomNumber = 1,
                            CategoryID = context.Categories.First().CategoryID, 
                            GeneralLedger = "GL001",
                            ItemType = ItemType.Consumable,
                            Comments = "Glue sticks for everyone",
                            PricePerUnit = 1.50F,
                            QuantityAvailable = 100,
                            MinimumQuantity = 10
                        }
                    );
                }

                if (!context.InventoryItems.OfType<NonConsumable>().Any())
                {
                    context.InventoryItems.AddRange(
                        new NonConsumable
                        {
                            Name = "Laptop",
                            Description = "Dell Laptop",
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.UtcNow,
                            EditedDate = DateTime.UtcNow,
                            RoomNumber = 1,
                            CategoryID = context.Categories.First().CategoryID, 
                            GeneralLedger = "GL002",
                            ItemType = ItemType.NonConsumable,
                            Comments = "Dell Laptop for staff",
                            AssetTag = "A001"
                        }
                    );
                }

                context.SaveChanges();

                if (!context.Departments.Any())
                {
                    context.Departments.AddRange(
                        new Department
                        {
                            DeptName = "Learning Center",
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.UtcNow,
                            EditedDate = DateTime.UtcNow,
                        }
                    );
                }

                if (!context.InventoryItems.Any())
                {
                    context.InventoryItems.AddRange(
                        new InventoryItem
                        {
                            Name = "Glue Sticks",
                            Description = "We love glue sticks at Hope Haven",
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.UtcNow,
                            EditedDate = DateTime.UtcNow,
                            RoomNumber = 1,
                            CategoryID = 1,
                            GeneralLedger = "GL001",
                            ItemType = ItemType.Consumable,
                            Comments = "Glue sticks for everyone",
                        }
                    );
                }

                if (!context.CheckOuts.Any())
                {
                    context.CheckOuts.AddRange(
                        new CheckOut
                        {
                            UserID = 1,
                            ItemID = 1,
                            CheckOutDate = DateTime.UtcNow,
                            ReturnDate = DateTime.UtcNow.AddDays(10),
                            Price = 1000,
                            DepartmentID = 1,
                            Notes = "Handle with care, return on time."
                        }
                    );
                }

                if (!context.CheckIns.Any())
                {
                    context.CheckIns.AddRange(
                        new CheckIn
                        {
                            AssetTag = "A001",
                            CheckInDate = DateTime.UtcNow,
                            UserID = 1
                        }
                    );
                }

                if (!context.InventoryIntakes.Any())
                {
                    context.InventoryIntakes.AddRange(
                        new InventoryIntake
                        {
                            Quantity = 10,
                            IntakeDate = DateTime.UtcNow
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}