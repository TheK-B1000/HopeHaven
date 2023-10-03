using Microsoft.EntityFrameworkCore;
using System;

namespace Collaborative_Resource_Management_System.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Consumable> Consumables { get; set; }
        public DbSet<NonConsumable> NonConsumables { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<InventoryIntake> InventoryIntakes { get; set; }

    }
}
