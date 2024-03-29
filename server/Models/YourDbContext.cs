using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        // public DbSet<RollOfSteel> RollsOfSteel { get; set; } REMOVED
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TruckUser> TruckUsers { get; set; }
        // public DbSet<OrderRoll> OrderRolls { get; set; } REMOVED
        public DbSet<TruckOrderAssignment> TruckOrderAssignments { get; set; }

        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            //#################################################################################### COMMENT OUT START FOR NO DATABASE OBJECT SEEDING

               // Area

            var area1Id = "A1";
            var area2Id = "A2";
            var area3Id = "A3";

            modelBuilder.Entity<Area>().HasData(
                new Area { AreaId = area1Id, Name = "North Warehouse" },
                new Area { AreaId = area2Id, Name = "South Warehouse" },
                new Area { AreaId = area3Id, Name = "East warehouse"}
            );

            // Location;

            var location1Id = "L1";
            var location2Id = "L2";
            var location3Id = "L3";
            var location4Id = "L4";

            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = location1Id, AreaId = area1Id, LocationType = LocationType.Storage },
                new Location { LocationId = location2Id, AreaId = area2Id, LocationType = LocationType.Machine },
                new Location { LocationId = location3Id, AreaId = area3Id, LocationType = LocationType.Machine },
                new Location { LocationId = location4Id, AreaId = area3Id, LocationType = LocationType.Machine }
            );

            // User
            var user1Id = "U1-" + Guid.NewGuid().ToString();
            var user2Id = "U2-" + Guid.NewGuid().ToString();
            var user3Id = "U3-" + Guid.NewGuid().ToString();

            string plainPassword1 = "yourPasswordHere";
            // Hash the password
            string plainPassword2 = "hashedPassword2";
            string plainPassword3 = "hej";

            modelBuilder.Entity<User>().HasData(
                new User { UserId = user1Id, Username = "adminUser", Role = User.UserRole.Admin, PasswordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword1) },
                new User { UserId = user2Id, Username = "standardUser", Role = User.UserRole.User, PasswordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword2) },
                new User { UserId = user3Id, Username = "hej", Role = User.UserRole.Admin, PasswordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword3) }
            );

            // Truck
            var truck1Id = "T1";
            var truck2Id = "T2";
            var truckregnr1 = "abc123";
            var truckregnr2 = "bcd234";

            modelBuilder.Entity<Truck>().HasData(
                new Truck { TruckId = truck1Id, CurrentAreaId = area1Id, registrationnumber = truckregnr1 },
                new Truck { TruckId = truck2Id, CurrentAreaId = area2Id, registrationnumber = truckregnr2 }
            );

            // Order
            var order1Id = "O1-" + Guid.NewGuid().ToString();

            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = order1Id, UserID = user1Id, OrderStatus = OrderStatus.Pending, FromLocId = location1Id, ToLocId = location2Id, Pieces = 7, CreatedAt = DateTime.UtcNow }
            );

            // TruckUser
            modelBuilder.Entity<TruckUser>().HasData(
                new TruckUser { TruckUserId = 1, TruckId = truck1Id, UserId = user1Id, IsAssigned = true, AssignedAt = DateTime.UtcNow }
            );

            // Define a counter variable to generate incrementing IDs
            int truckOrderAssignmentCounter = 1;

            // Seeding method
            modelBuilder.Entity<TruckOrderAssignment>().HasData(
                new TruckOrderAssignment { TruckOrderAssignmentId = truckOrderAssignmentCounter++, TruckId = truck1Id, OrderId = order1Id }
                // Add more TruckOrderAssignment entries here as needed
            );


//################################################################################################################ COMMENT OUT END FOR NO DATABASE OBJECT SEEDING
            // Ensure Area Name is unique
            modelBuilder.Entity<Area>()
                .HasIndex(a => a.Name)
                .IsUnique();

            // Ensure User Username is unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            // One Area, Many Trucks
            modelBuilder.Entity<Truck>()
                .HasOne<Area>(t => t.CurrentArea)
                .WithMany(a => a.Trucks)
                .HasForeignKey(t => t.CurrentAreaId)
                .IsRequired(false);
            
            // One Area, Many Locations
            modelBuilder.Entity<Location>()
                .HasOne(l => l.Area)
                .WithMany(a => a.Locations)
                .HasForeignKey(l => l.AreaId)
                .OnDelete(DeleteBehavior.Cascade);

            // One Location, Many RollsOfSteel
            // modelBuilder.Entity<RollOfSteel>()
            //     .HasOne<Location>(r => r.CurrentLocation)
            //     .WithMany(l => l.RollsOfSteel)
            //     .HasForeignKey(r => r.CurrentLocationId);
            
            // One Location, Many Orders
            modelBuilder.Entity<Order>()
                .HasOne<Location>(o => o.ToLocation)
                .WithMany(l => l.ToOrders)
                .HasForeignKey(o => o.ToLocId);

            modelBuilder.Entity<Order>()
                .HasOne<Location>(o => o.FromLocation)
                .WithMany(l => l.FromOrders)
                .HasForeignKey(o => o.FromLocId);

            // One User, Many Orders
            modelBuilder.Entity<Order>()
                .HasOne<User>(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID);
            
            // REMOVED
            // modelBuilder.Entity<Order>()
            // .HasMany(o => o.OrderRolls)
            // .WithOne(or => or.Order)
            // .HasForeignKey(or => or.OrderId)
            // .OnDelete(DeleteBehavior.Cascade);

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


            // REMOVED ORDERROLL
            // modelBuilder.Entity<OrderRoll>()
            //     .HasKey(or => or.OrderRollId);
                
            // modelBuilder.Entity<OrderRoll>()
            //     .HasOne(or => or.RollOfSteel)
            //     .WithMany(r => r.OrderRolls)
            //     .HasForeignKey(or => or.RollOfSteelId);

            // modelBuilder.Entity<OrderRoll>()
            //     .HasOne(or => or.Order)
            //     .WithMany(o => o.OrderRolls)
            //     .HasForeignKey(or => or.OrderId);

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
