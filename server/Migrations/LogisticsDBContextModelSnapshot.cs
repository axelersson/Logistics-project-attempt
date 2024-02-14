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

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DestinationId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SourceId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("OrderId");

                    b.HasIndex("DestinationId");

                    b.HasIndex("SourceId");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderRoll", b =>
                {
                    b.Property<int>("OrderRollId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RollOfSteelId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("OrderRollId");

                    b.HasIndex("OrderId");

                    b.HasIndex("RollOfSteelId");

                    b.ToTable("OrderRolls");
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

                    b.Property<string>("CurrentAreaId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("TruckId");

                    b.HasIndex("CurrentAreaId");

                    b.ToTable("Trucks");
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

                    b.HasOne("Location", "SourceLocation")
                        .WithMany("SourceOrders")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DestinationLocation");

                    b.Navigation("SourceLocation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderRoll", b =>
                {
                    b.HasOne("Order", "Order")
                        .WithMany("OrderRolls")
                        .HasForeignKey("OrderId");

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
                    b.Navigation("DestinationOrders");

                    b.Navigation("RollsOfSteel");

                    b.Navigation("SourceOrders");
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
                });
#pragma warning restore 612, 618
        }
    }
}
