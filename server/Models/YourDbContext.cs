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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship between Truck and Area
            modelBuilder.Entity<Truck>()
                .HasOne<Area>(t => t.CurrentArea)
                .WithMany(a => a.Trucks)
                .HasForeignKey(t => t.CurrentAreaId);

            // Configure many-to-many relationship between RollOfSteel and Order
            modelBuilder.Entity<OrderRollOfSteel>()
                .HasKey(or => new { or.OrderId, or.RollOfSteelId });
            modelBuilder.Entity<OrderRollOfSteel>()
                .HasOne(or => or.Order)
                .WithMany(o => o.OrderRollsOfSteel)
                .HasForeignKey(or => or.OrderId);
            modelBuilder.Entity<OrderRollOfSteel>()
                .HasOne(or => or.RollOfSteel)
                .WithMany(r => r.OrderRollsOfSteel)
                .HasForeignKey(or => or.RollOfSteelId);

            // Configure one-to-many relationship between Area and Location
            modelBuilder.Entity<Location>()
                .HasOne<Area>(l => l.Area)
                .WithMany(a => a.Locations)
                .HasForeignKey(l => l.AreaId);

            // ... other relationship configurations
        }
    }
}
