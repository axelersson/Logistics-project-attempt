using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LogisticsApp.Data // Change to your actual namespace
{
    public class LogisticsDBContext : DbContext
    {
        public LogisticsDBContext(DbContextOptions<LogisticsDBContext> options) : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<RollOfSteel> RollsOfSteel { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderRollOfSteel> OrderRollsOfSteel { get; set; } // Added DbSet for OrderRollOfSteel

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Existing relationship configurations...

            // Configure many-to-many relationship between RollOfSteel and Order via OrderRollOfSteel
            modelBuilder.Entity<OrderRollOfSteel>()
                .HasKey(or => new { or.OrderId, or.RollOfSteelId }); // Composite key

            modelBuilder.Entity<OrderRollOfSteel>()
                .HasOne(or => or.Order)
                .WithMany(o => o.OrderRollsOfSteel) // Assuming you add this ICollection to the Order model
                .HasForeignKey(or => or.OrderId);

            modelBuilder.Entity<OrderRollOfSteel>()
                .HasOne(or => or.RollOfSteel)
                .WithMany(r => r.OrderRollsOfSteel) // Assuming you add this ICollection to the RollOfSteel model
                .HasForeignKey(or => or.RollOfSteelId);

            // ... other relationship configurations
        }
    }
}
