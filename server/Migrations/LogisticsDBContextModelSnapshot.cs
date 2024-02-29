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
                            AreaId = "A1",
                            Name = "North Warehouse"
                        },
                        new
                        {
                            AreaId = "A2",
                            Name = "South Warehouse"
                        },
                        new
                        {
                            AreaId = "A3",
                            Name = "East warehouse"
                        });
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.Property<string>("LocationId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AreaId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("LocationType")
                        .HasColumnType("int");

                    b.HasKey("LocationId");

                    b.HasIndex("AreaId");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            LocationId = "L1",
                            AreaId = "A1",
                            LocationType = 0
                        },
                        new
                        {
                            LocationId = "L2",
                            AreaId = "A2",
                            LocationType = 1
                        },
                        new
                        {
                            LocationId = "L3",
                            AreaId = "A3",
                            LocationType = 1
                        },
                        new
                        {
                            LocationId = "L4",
                            AreaId = "A3",
                            LocationType = 1
                        });
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FromLocId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<int?>("OrderType")
                        .HasColumnType("int");

                    b.Property<int>("Pieces")
                        .HasColumnType("int");

                    b.Property<string>("ToLocId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.HasKey("OrderId");

                    b.HasIndex("FromLocId");

                    b.HasIndex("ToLocId");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = "O1-98d35218-ecf6-4cd9-92d2-7c82f0d8f52a",
                            CreatedAt = new DateTime(2024, 2, 29, 9, 55, 39, 647, DateTimeKind.Utc).AddTicks(7811),
                            FromLocId = "L1",
                            OrderStatus = 0,
                            Pieces = 7,
                            ToLocId = "L2",
                            UserID = "U1-ed6747c9-0a63-4538-94de-0df5a5631989"
                        });
                });

            modelBuilder.Entity("Truck", b =>
                {
                    b.Property<string>("TruckId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CurrentAreaId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("registrationnumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TruckId");

                    b.HasIndex("CurrentAreaId");

                    b.ToTable("Trucks");

                    b.HasData(
                        new
                        {
                            TruckId = "T1-653334e5-4801-4e72-a0e9-819a7e8b2e34",
                            CurrentAreaId = "A1",
                            registrationnumber = "abc123"
                        },
                        new
                        {
                            TruckId = "T2-e48c65b4-e3ce-4306-b814-8d13e53771c4",
                            CurrentAreaId = "A2",
                            registrationnumber = "bcd234"
                        });
                });

            modelBuilder.Entity("TruckOrderAssignment", b =>
                {
                    b.Property<int>("TruckOrderAssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("AssignedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAssigned")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TruckId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UnassignedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("TruckOrderAssignmentId");

                    b.HasIndex("OrderId");

                    b.HasIndex("TruckId");

                    b.ToTable("TruckOrderAssignments");

                    b.HasData(
                        new
                        {
                            TruckOrderAssignmentId = 1,
                            AssignedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAssigned = true,
                            OrderId = "O1-98d35218-ecf6-4cd9-92d2-7c82f0d8f52a",
                            TruckId = "T1-653334e5-4801-4e72-a0e9-819a7e8b2e34"
                        });
                });

            modelBuilder.Entity("TruckUser", b =>
                {
                    b.Property<int>("TruckUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AssignedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAssigned")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TruckId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("UnassignedAt")
                        .HasColumnType("datetime(6)");

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
                            AssignedAt = new DateTime(2024, 2, 29, 9, 55, 39, 647, DateTimeKind.Utc).AddTicks(7858),
                            IsAssigned = true,
                            TruckId = "T1-653334e5-4801-4e72-a0e9-819a7e8b2e34",
                            UserId = "U1-ed6747c9-0a63-4538-94de-0df5a5631989"
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
                            UserId = "U1-ed6747c9-0a63-4538-94de-0df5a5631989",
                            PasswordHash = "$2a$11$RtSQzMxmQSRq4lsXxopNxOp1xUnkfgGergWzfdbbpY6/HlJqCxDQ2",
                            Role = 0,
                            Username = "adminUser"
                        },
                        new
                        {
                            UserId = "U2-8e4cc73a-650b-4877-8a83-ca2a10025fa2",
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
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("Location", "FromLocation")
                        .WithMany("FromOrders")
                        .HasForeignKey("FromLocId");

                    b.HasOne("Location", "ToLocation")
                        .WithMany("ToOrders")
                        .HasForeignKey("ToLocId");

                    b.HasOne("User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID");

                    b.Navigation("FromLocation");

                    b.Navigation("ToLocation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Truck", b =>
                {
                    b.HasOne("Area", "CurrentArea")
                        .WithMany("Trucks")
                        .HasForeignKey("CurrentAreaId");

                    b.Navigation("CurrentArea");
                });

            modelBuilder.Entity("TruckOrderAssignment", b =>
                {
                    b.HasOne("Order", "Order")
                        .WithMany("TruckOrderAssignments")
                        .HasForeignKey("OrderId");

                    b.HasOne("Truck", "Truck")
                        .WithMany("TruckOrderAssignments")
                        .HasForeignKey("TruckId");

                    b.Navigation("Order");

                    b.Navigation("Truck");
                });

            modelBuilder.Entity("TruckUser", b =>
                {
                    b.HasOne("Truck", "Truck")
                        .WithMany("TruckUsers")
                        .HasForeignKey("TruckId");

                    b.HasOne("User", "User")
                        .WithMany("TruckUsers")
                        .HasForeignKey("UserId");

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
                    b.Navigation("FromOrders");

                    b.Navigation("ToOrders");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Navigation("TruckOrderAssignments");
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
                });
#pragma warning restore 612, 618
        }
    }
}
