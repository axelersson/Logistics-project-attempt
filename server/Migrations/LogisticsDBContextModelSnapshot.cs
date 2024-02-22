﻿// <auto-generated />
using System;
using LogisticsApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(LogisticsDBContext))]
    partial class LogisticsDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Area", b =>
                {
                    b.Property<string>("AreaId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("AreaId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Areas");

                    b.HasData(
                        new
                        {
                            AreaId = "A1-c9eba881-d848-4144-b3ab-0b6dd66640c0",
                            Name = "North Warehouse"
                        },
                        new
                        {
                            AreaId = "A2-d733e988-5dc7-40bb-9689-421b3a5a446c",
                            Name = "South Warehouse"
                        });
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.Property<string>("LocationId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AreaId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("LocationType")
                        .HasColumnType("int");

                    b.HasKey("LocationId");

                    b.HasIndex("AreaId");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            LocationId = "L1-94a03ed3-0122-46ee-af28-fb164db73958",
                            AreaId = "A1-c9eba881-d848-4144-b3ab-0b6dd66640c0",
                            LocationType = 0
                        },
                        new
                        {
                            LocationId = "L2-3dc16611-2cc3-4252-9043-a32f5a2ea915",
                            AreaId = "A2-d733e988-5dc7-40bb-9689-421b3a5a446c",
                            LocationType = 1
                        });
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DestinationId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("OrderId");

                    b.HasIndex("DestinationId");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = "O1-174f1606-190b-4f97-b5a8-d7b2ae73f7d5",
                            CreatedAt = new DateTime(2024, 2, 21, 10, 1, 19, 649, DateTimeKind.Utc).AddTicks(7630),
                            DestinationId = "L1-94a03ed3-0122-46ee-af28-fb164db73958",
                            OrderStatus = 0,
                            UserID = "U1-40277a05-aeb9-4ed8-9305-de46d3f5b964"
                        });
                });

            modelBuilder.Entity("OrderRoll", b =>
                {
                    b.Property<int>("OrderRollId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("OrderRollStatus")
                        .HasColumnType("int");

                    b.Property<string>("RollOfSteelId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("OrderRollId");

                    b.HasIndex("OrderId");

                    b.HasIndex("RollOfSteelId");

                    b.ToTable("OrderRolls");

                    b.HasData(
                        new
                        {
                            OrderRollId = 1,
                            OrderId = "O1-174f1606-190b-4f97-b5a8-d7b2ae73f7d5",
                            OrderRollStatus = 0,
                            RollOfSteelId = "R1-c4a3be2a-6e7c-4c84-99e6-7e764a6107c9"
                        });
                });

            modelBuilder.Entity("RollOfSteel", b =>
                {
                    b.Property<string>("RollOfSteelId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CurrentLocationId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("RollStatus")
                        .HasColumnType("int");

                    b.HasKey("RollOfSteelId");

                    b.HasIndex("CurrentLocationId");

                    b.ToTable("RollsOfSteel");

                    b.HasData(
                        new
                        {
                            RollOfSteelId = "R1-c4a3be2a-6e7c-4c84-99e6-7e764a6107c9",
                            CurrentLocationId = "L1-94a03ed3-0122-46ee-af28-fb164db73958",
                            RollStatus = 1
                        });
                });

            modelBuilder.Entity("Truck", b =>
                {
                    b.Property<string>("TruckId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CurrentAreaId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("TruckId");

                    b.HasIndex("CurrentAreaId");

                    b.HasIndex("UserId");

                    b.ToTable("Trucks");

                    b.HasData(
                        new
                        {
                            TruckId = "T1-0b0bce22-7bcc-4b5e-b16f-155a395dfa6a",
                            CurrentAreaId = "A1-c9eba881-d848-4144-b3ab-0b6dd66640c0"
                        },
                        new
                        {
                            TruckId = "T2-879d0f1a-fb48-4f83-a0b3-cdea5582feed",
                            CurrentAreaId = "A2-d733e988-5dc7-40bb-9689-421b3a5a446c"
                        });
                });

            modelBuilder.Entity("TruckOrderAssignment", b =>
                {
                    b.Property<int>("TruckOrderAssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("AssignmentAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TruckId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UnassignmentAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TruckOrderAssignmentId");

                    b.HasIndex("OrderId");

                    b.HasIndex("TruckId");

                    b.ToTable("TruckOrderAssignments");

                    b.HasData(
                        new
                        {
                            TruckOrderAssignmentId = 1,
                            OrderId = "O1-174f1606-190b-4f97-b5a8-d7b2ae73f7d5",
                            TruckId = "T1-0b0bce22-7bcc-4b5e-b16f-155a395dfa6a"
                        });
                });

            modelBuilder.Entity("TruckUser", b =>
                {
                    b.Property<int>("TruckUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateAssigned")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateUnassigned")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAssigned")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TruckId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("TruckUserId");

                    b.HasIndex("TruckId");

                    b.HasIndex("UserId");

                    b.ToTable("TruckUsers");

                    b.HasData(
                        new
                        {
                            TruckUserId = 1,
                            DateAssigned = new DateTime(2024, 2, 21, 10, 1, 19, 649, DateTimeKind.Utc).AddTicks(7650),
                            IsAssigned = true,
                            TruckId = "T1-0b0bce22-7bcc-4b5e-b16f-155a395dfa6a",
                            UserId = "U1-40277a05-aeb9-4ed8-9305-de46d3f5b964"
                        });
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = "U1-40277a05-aeb9-4ed8-9305-de46d3f5b964",
                            PasswordHash = "hashedPassword1",
                            Role = 0,
                            Username = "adminUser"
                        },
                        new
                        {
                            UserId = "U2-dc142e9d-a170-414e-837a-7682d3a16a0b",
                            PasswordHash = "hashedPassword2",
                            Role = 1,
                            Username = "standardUser"
                        });
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.HasOne("Area", "Area")
                        .WithMany("Locations")
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("Location", "DestinationLocation")
                        .WithMany("DestinationOrders")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DestinationLocation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderRoll", b =>
                {
                    b.HasOne("Order", "Order")
                        .WithMany("OrderRolls")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RollOfSteel", "RollOfSteel")
                        .WithMany("OrderRolls")
                        .HasForeignKey("RollOfSteelId");

                    b.Navigation("Order");

                    b.Navigation("RollOfSteel");
                });

            modelBuilder.Entity("RollOfSteel", b =>
                {
                    b.HasOne("Location", "CurrentLocation")
                        .WithMany("RollsOfSteel")
                        .HasForeignKey("CurrentLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentLocation");
                });

            modelBuilder.Entity("Truck", b =>
                {
                    b.HasOne("Area", "CurrentArea")
                        .WithMany("Trucks")
                        .HasForeignKey("CurrentAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", null)
                        .WithMany("Trucks")
                        .HasForeignKey("UserId");

                    b.Navigation("CurrentArea");
                });

            modelBuilder.Entity("TruckOrderAssignment", b =>
                {
                    b.HasOne("Order", "Order")
                        .WithMany("TruckOrderAssignments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Truck", "Truck")
                        .WithMany("TruckOrderAssignments")
                        .HasForeignKey("TruckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Truck");
                });

            modelBuilder.Entity("TruckUser", b =>
                {
                    b.HasOne("Truck", "Truck")
                        .WithMany("TruckUsers")
                        .HasForeignKey("TruckId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("User", "User")
                        .WithMany("TruckUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Truck");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Area", b =>
                {
                    b.Navigation("Locations");

                    b.Navigation("Trucks");
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.Navigation("DestinationOrders");

                    b.Navigation("RollsOfSteel");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Navigation("OrderRolls");

                    b.Navigation("TruckOrderAssignments");
                });

            modelBuilder.Entity("RollOfSteel", b =>
                {
                    b.Navigation("OrderRolls");
                });

            modelBuilder.Entity("Truck", b =>
                {
                    b.Navigation("TruckOrderAssignments");

                    b.Navigation("TruckUsers");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("TruckUsers");

                    b.Navigation("Trucks");
                });
#pragma warning restore 612, 618
        }
    }
}
