﻿// <auto-generated />
using LogisticsApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace server.Migrations
{
    [DbContext(typeof(LogisticsDBContext))]
    [Migration("20240202130145_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Area", b =>
                {
                    b.Property<string>("AreaId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LocationIds")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

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

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CurrentOccupancy")
                        .HasColumnType("int");

                    b.HasKey("LocationId");

                    b.HasIndex("AreaId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Machine", b =>
                {
                    b.Property<string>("MachineId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LocationId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MachineId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AssociatedRollsIds")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DestinationMachineId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SourceMachineId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrderRollOfSteel", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RollOfSteelId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("OrderRollOfSteelOrderId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("OrderRollOfSteelRollOfSteelId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("OrderId", "RollOfSteelId");

                    b.HasIndex("RollOfSteelId");

                    b.HasIndex("OrderRollOfSteelOrderId", "OrderRollOfSteelRollOfSteelId");

                    b.ToTable("OrderRollOfSteel");
                });

            modelBuilder.Entity("RollOfSteel", b =>
                {
                    b.Property<string>("RollOfSteelId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CurrentLocationId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DestinationLocationId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RollOfSteelId");

                    b.ToTable("RollsOfSteel");
                });

            modelBuilder.Entity("Truck", b =>
                {
                    b.Property<string>("TruckId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CurrentAreaId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("TruckId");

                    b.HasIndex("CurrentAreaId");

                    b.ToTable("Trucks");
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

            modelBuilder.Entity("OrderRollOfSteel", b =>
                {
                    b.HasOne("Order", "Order")
                        .WithMany("OrderRollsOfSteel")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RollOfSteel", "RollOfSteel")
                        .WithMany("OrderRollsOfSteel")
                        .HasForeignKey("RollOfSteelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderRollOfSteel", null)
                        .WithMany("OrderRollsOfSteel")
                        .HasForeignKey("OrderRollOfSteelOrderId", "OrderRollOfSteelRollOfSteelId");

                    b.Navigation("Order");

                    b.Navigation("RollOfSteel");
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

            modelBuilder.Entity("Area", b =>
                {
                    b.Navigation("Locations");

                    b.Navigation("Trucks");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Navigation("OrderRollsOfSteel");
                });

            modelBuilder.Entity("OrderRollOfSteel", b =>
                {
                    b.Navigation("OrderRollsOfSteel");
                });

            modelBuilder.Entity("RollOfSteel", b =>
                {
                    b.Navigation("OrderRollsOfSteel");
                });
#pragma warning restore 612, 618
        }
    }
}