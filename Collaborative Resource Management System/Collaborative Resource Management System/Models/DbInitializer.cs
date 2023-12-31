using Collaborative_Resource_Management_System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;



namespace Collaborative_Resource_Management_System.Models
{
    public static class DbInitializer
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServiceProvider = scope.ServiceProvider;
                var context = scopedServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scopedServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scopedServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                SeedRoles(roleManager);

                if (!context.Users.Any())
                {
                    var user = new IdentityUser
                    {
                        UserName = "BenyFarfan",
                        Email = "beny@example.com",
                        PhoneNumber = "9042303318",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        LockoutEnabled = false
                    };

                    var passwordHasher = new PasswordHasher<IdentityUser>();
                    user.PasswordHash = passwordHasher.HashPassword(user, "123456");

                    var createResult = await userManager.CreateAsync(user);
                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        foreach (var error in createResult.Errors)
                        {
                            Console.WriteLine($"Error: {error.Description}");
                        }
                    }
                }

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category
                        {
                            Name = "Electronics",
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.UtcNow,
                            EditedDate = DateTime.UtcNow,
                            IsActive = true
                        }
                    );
                }

                if (!context.Departments.Any())
                {
                    context.Departments.AddRange(
                        new Department
                        {
                            Name = "Learning Center",
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.UtcNow,
                            EditedDate = DateTime.UtcNow,
                            IsActive = true
                        }
                    );
                }

                context.SaveChanges();

                if (!context.InventoryItems.OfType<Consumable>().Any())
                {
                    context.InventoryItems.AddRange(
                        new Consumable
                        {
                            Image = "GlueSticks.jpg",
                            Name = "Glue Sticks",
                            Description = "We love glue sticks",
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.UtcNow,
                            EditedDate = DateTime.UtcNow,
                            RoomNumber = 1,
                            CategoryID = context.Categories.First().CategoryID,
                            ItemType = ItemType.Consumable,
                            Comments = "Glue sticks for everyone",
                            PricePerUnit = 1.50F,
                            QuantityAvailable = 100,
                            MinimumQuantity = 10,
                            IsActive = true
                        }
                    );
                }

                if (!context.InventoryItems.OfType<NonConsumable>().Any())
                {
                    context.InventoryItems.AddRange(
                        new NonConsumable
                        {
                            Image = "Dell.jpg",
                            Name = "Laptop",
                            Description = "Dell Laptop",
                            CreatedBy = "Stella",
                            EditedBy = "Kim",
                            CreatedDate = DateTime.UtcNow,
                            EditedDate = DateTime.UtcNow,
                            RoomNumber = 1,
                            CategoryID = context.Categories.First().CategoryID,
                            ItemType = ItemType.NonConsumable,
                            Comments = "Dell Laptop for staff",
                            AssetTag = "A001",
                            IsActive = true
                        }
                    );
                }

                context.SaveChanges();

                if (!context.CheckOuts.Any())
                {
                    context.CheckOuts.AddRange(
                        new CheckOut
                        {
                            InventoryItemID = 1,
                            CheckOutDate = DateTime.UtcNow,
                            ReturnDate = DateTime.UtcNow.AddDays(10),
                            TotalPrice = 1000,
                            DepartmentID = context.Departments.First().DepartmentID,
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
                            UserID = "e93f1b7a-73df-4419-bae4-5e45c063e24f"
                        }
                    );
                }
                context.SaveChanges();
         
                if (!context.Reports.Any())
                {
                    context.Reports.Add(
                        new Report
                        {
                            ReportName = "GlueSticks Report",
                            ReportDescription = "Gluesticks for everyone",
                            ReportDate = DateTime.UtcNow,
                            UserID = "e93f1b7a-73df-4419-bae4-5e45c063e24f"
                        }
                );
                context.SaveChanges();
            }
        }
    }
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Editor", "Staff" };
            foreach (var roleName in roleNames)
            {
                var roleExist = roleManager.RoleExistsAsync(roleName).Result;
                if (!roleExist)
                {
                    roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
                }
            }
        }
    }
}