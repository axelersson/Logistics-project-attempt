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
                .WithMany()
                .HasForeignKey(t => t.CurrentAreaId);

            // Configure many-to-many relationship between Truck and User
            modelBuilder.Entity<Truck>()
                .HasMany(t => t.Users)
                .WithMany(u => u.Trucks)
                .UsingEntity("TruckUser");

            // Configure many-to-many relationship between RollOfSteel and Order
            modelBuilder.Entity<Order>()
                .HasMany(or => or.RollsOfSteel)
                .WithMany(r => r.Orders)
                .UsingEntity("OrderRollsOfSteel");

            // Configure one-to-many relationship between Area and Location
            modelBuilder.Entity<Location>()
                .HasOne<Area>(l => l.Area)
                .WithMany()
                .HasForeignKey(l => l.AreaId);

            //Configure many-to-many relationship between Truck and Order
            modelBuilder.Entity<Truck>()
                .HasMany(t => t.Orders)
                .WithMany(o => o.Trucks)
                .UsingEntity("TruckOrder");

            // Configure one-to-many relationship between Location and RollOfSteel
            modelBuilder.Entity<RollOfSteel>()
                .HasOne<Location>(r => r.CurrentLocation)
                .WithMany()
                .HasForeignKey(r => r.CurrentLocationId);
        }
    }
}
