using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace LogisticsApp.Data // Change to your actual namespace
{
    public class LogisticsDBContext : DbContext
    {
        public LogisticsDBContext(DbContextOptions<LogisticsDBContext> options) : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<RollOfSteel> RollsOfSteel { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TruckUser> TruckUsers { get; set; }
        public DbSet<OrderRoll> OrderRolls { get; set; }
        public DbSet<TruckOrderAssignment> TruckOrderAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One Area, Many Trucks
            modelBuilder.Entity<Truck>()
                .HasOne<Area>(t => t.CurrentArea)
                .WithMany(a => a.Trucks)
                .HasForeignKey(t => t.CurrentAreaId);
            
            // One Area, Many Locations
            modelBuilder.Entity<Location>()
                .HasOne(l => l.Area)
                .WithMany(a => a.Locations)
                .HasForeignKey(l => l.AreaId);

            // One Location, Many RollsOfSteel
            modelBuilder.Entity<RollOfSteel>()
                .HasOne<Location>(r => r.CurrentLocation)
                .WithMany(l => l.RollsOfSteel)
                .HasForeignKey(r => r.CurrentLocationId);
            
            // One Location, Many Orders
            //From
            modelBuilder.Entity<Order>()
                .HasOne<Location>(o => o.SourceLocation)
                .WithMany(l => l.SourceOrders)
                .HasForeignKey(o => o.SourceId);
            // To
            modelBuilder.Entity<Order>()
                .HasOne<Location>(o => o.DestinationLocation)
                .WithMany(l => l.DestinationOrders)
                .HasForeignKey(o => o.DestinationId);

            // One User, Many Orders
            modelBuilder.Entity<Order>()
                .HasOne<User>(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID);

            // INTERMEDIATE TABLES
            // Configure TruckUser

            modelBuilder.Entity<TruckUser>()
                .HasOne<Truck>(tu => tu.Truck)
                .WithMany(t => t.TruckUsers)
                .HasForeignKey(tu => tu.TruckId);

            modelBuilder.Entity<TruckUser>()
                .HasOne<User>(tu => tu.User)
                .WithMany(u => u.TruckUsers)
                .HasForeignKey(tu => tu.UserId);

            // Configure OrderRoll

            modelBuilder.Entity<OrderRoll>()
                .HasOne(or => or.RollOfSteel)
                .WithMany(r => r.OrderRolls)
                .HasForeignKey(or => or.RollOfSteelId);

            modelBuilder.Entity<OrderRoll>()
                .HasOne(or => or.Order)
                .WithMany(o => o.OrderRolls)
                .HasForeignKey(or => or.OrderId);

            // Configure TruckOrderAssignment

            modelBuilder.Entity<TruckOrderAssignment>()
                .HasOne(toa => toa.Truck)
                .WithMany(t => t.TruckOrderAssignments)
                .HasForeignKey(toa => toa.TruckId);

            modelBuilder.Entity<TruckOrderAssignment>()
                .HasOne(toa => toa.Order)
                .WithMany(o => o.TruckOrderAssignments)
                .HasForeignKey(toa => toa.OrderId);

        }
    }
}
