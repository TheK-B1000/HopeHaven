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
                            Active = true
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
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                            Active = true
                        }
                    );
                }

                if (!context.Departments.Any())
                {
                    context.Departments.AddRange(
                        new Department
                        {
                            DeptName = "Learning Center",
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.Now,
                            EditedDate = DateTime.Now,
                            Active = true
                            
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
                            Active = true
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
                            CheckInDate = DateTime.Now,
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
                            IntakeDate = DateTime.Now
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}