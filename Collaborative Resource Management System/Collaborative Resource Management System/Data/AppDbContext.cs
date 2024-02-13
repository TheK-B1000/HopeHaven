using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Collaborative_Resource_Management_System.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InventoryItem>()
                .OwnsOne(i => i.Consumable)
                .WithOwner();

            modelBuilder.Entity<InventoryItem>()
                .OwnsOne(i => i.NonConsumable)
                .WithOwner();
        }
    }
}