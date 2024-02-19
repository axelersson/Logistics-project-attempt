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

                    b.HasKey("AreaId");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.Property<string>("LocationId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AreaId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LocationId");

                    b.HasIndex("AreaId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Machine", b =>
                {
                    b.Property<string>("MachineId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AreaID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MachineId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DestinationId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SourceId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderRollsOfSteel", b =>
                {
                    b.Property<string>("OrdersOrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RollsOfSteelRollOfSteelId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("OrdersOrderId", "RollsOfSteelRollOfSteelId");

                    b.HasIndex("RollsOfSteelRollOfSteelId");

                    b.ToTable("OrderRollsOfSteel");
                });

            modelBuilder.Entity("RollOfSteel", b =>
                {
                    b.Property<string>("RollOfSteelId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CurrentLocationId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("RollOfSteelId");

                    b.HasIndex("CurrentLocationId");

                    b.ToTable("RollsOfSteel");
                });

            modelBuilder.Entity("Truck", b =>
                {
                    b.Property<string>("TruckId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AreaId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CurrentAreaId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

<<<<<<< Updated upstream
                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");
=======
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");
>>>>>>> Stashed changes

                    b.HasKey("TruckId");

                    b.HasIndex("AreaId");

                    b.HasIndex("CurrentAreaId");

                    b.HasIndex("UserId");

                    b.ToTable("Trucks");
                });

            modelBuilder.Entity("TruckOrder", b =>
                {
                    b.Property<string>("OrdersOrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TrucksTruckId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("OrdersOrderId", "TrucksTruckId");

                    b.HasIndex("TrucksTruckId");

                    b.ToTable("TruckOrder");
                });

            modelBuilder.Entity("TruckUser", b =>
                {
                    b.Property<string>("TrucksTruckId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UsersUserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("TrucksTruckId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("TruckUser");
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
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Location", b =>
                {
                    b.HasOne("Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("OrderRollsOfSteel", b =>
                {
                    b.HasOne("Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RollOfSteel", null)
                        .WithMany()
                        .HasForeignKey("RollsOfSteelRollOfSteelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RollOfSteel", b =>
                {
                    b.HasOne("Location", "CurrentLocation")
                        .WithMany()
                        .HasForeignKey("CurrentLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentLocation");
                });

            modelBuilder.Entity("Truck", b =>
                {
                    b.HasOne("Area", null)
                        .WithMany("Trucks")
                        .HasForeignKey("AreaId");

                    b.HasOne("Area", "CurrentArea")
                        .WithMany()
                        .HasForeignKey("CurrentAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", null)
                        .WithMany("Trucks")
                        .HasForeignKey("UserId");

                    b.Navigation("CurrentArea");
                });

            modelBuilder.Entity("TruckOrder", b =>
                {
                    b.HasOne("Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Truck", null)
                        .WithMany()
                        .HasForeignKey("TrucksTruckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TruckUser", b =>
                {
                    b.HasOne("Truck", null)
                        .WithMany()
                        .HasForeignKey("TrucksTruckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Area", b =>
                {
                    b.Navigation("Trucks");
                });
<<<<<<< Updated upstream
=======

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
>>>>>>> Stashed changes
#pragma warning restore 612, 618
        }
    }
}
