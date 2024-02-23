using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    AreaId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AreaId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Locations_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    TruckId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrentAreaId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.TruckId);
                    table.ForeignKey(
                        name: "FK_Trucks_Areas_CurrentAreaId",
                        column: x => x.CurrentAreaId,
                        principalTable: "Areas",
                        principalColumn: "AreaId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    ToLocId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FromLocId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pieces = table.Column<int>(type: "int", nullable: false),
                    OrderType = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Locations_FromLocId",
                        column: x => x.FromLocId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Locations_ToLocId",
                        column: x => x.ToLocId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TruckUsers",
                columns: table => new
                {
                    TruckUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TruckId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAssigned = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UnassignedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckUsers", x => x.TruckUserId);
                    table.ForeignKey(
                        name: "FK_TruckUsers_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "TruckId");
                    table.ForeignKey(
                        name: "FK_TruckUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TruckOrderAssignments",
                columns: table => new
                {
                    TruckOrderAssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TruckId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAssigned = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UnassignedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckOrderAssignments", x => x.TruckOrderAssignmentId);
                    table.ForeignKey(
                        name: "FK_TruckOrderAssignments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TruckOrderAssignments_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "TruckId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "AreaId", "Name" },
                values: new object[,]
                {
                    { "A1-9faa3e4d-663e-4c88-a80f-84e7279daf7a", "North Warehouse" },
                    { "A2-641ec683-3771-4b04-8993-cb0f37702591", "South Warehouse" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { "U1-78bb3bfe-d057-478c-b50f-f79e4ddb3d57", "$2a$11$valJRPFPqtCYAwil5TyaZOtPuOPpVMI6gv5yMwTUXDTtj.07kVJEW", 0, "adminUser" },
                    { "U2-f351a8af-003b-41e8-bf6d-72e657f794bc", "hashedPassword2", 1, "standardUser" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "AreaId", "LocationType" },
                values: new object[,]
                {
                    { "L1-eb7f8845-6092-44da-8a97-1dd20d9def9b", "A1-9faa3e4d-663e-4c88-a80f-84e7279daf7a", 0 },
                    { "L2-eecfc614-3b89-4ef9-bd4e-c640e76b3274", "A2-641ec683-3771-4b04-8993-cb0f37702591", 1 }
                });

            migrationBuilder.InsertData(
                table: "Trucks",
                columns: new[] { "TruckId", "CurrentAreaId" },
                values: new object[,]
                {
                    { "T1-e0a1a249-fbda-4c74-b4e0-c9d8d8c83261", "A1-9faa3e4d-663e-4c88-a80f-84e7279daf7a" },
                    { "T2-097b6c50-3728-4c93-9e65-5ffa91868e18", "A2-641ec683-3771-4b04-8993-cb0f37702591" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CompletedAt", "CreatedAt", "FromLocId", "OrderStatus", "OrderType", "Pieces", "ToLocId", "UserID" },
                values: new object[] { "O1-5a7bad59-2e44-4614-89c7-18679c6cc074", null, new DateTime(2024, 2, 22, 14, 34, 18, 91, DateTimeKind.Utc).AddTicks(7551), "L1-eb7f8845-6092-44da-8a97-1dd20d9def9b", 0, null, 7, "L2-eecfc614-3b89-4ef9-bd4e-c640e76b3274", "U1-78bb3bfe-d057-478c-b50f-f79e4ddb3d57" });

            migrationBuilder.InsertData(
                table: "TruckUsers",
                columns: new[] { "TruckUserId", "AssignedAt", "IsAssigned", "TruckId", "UnassignedAt", "UserId" },
                values: new object[] { 1, new DateTime(2024, 2, 22, 14, 34, 18, 91, DateTimeKind.Utc).AddTicks(7587), true, "T1-e0a1a249-fbda-4c74-b4e0-c9d8d8c83261", null, "U1-78bb3bfe-d057-478c-b50f-f79e4ddb3d57" });

            migrationBuilder.InsertData(
                table: "TruckOrderAssignments",
                columns: new[] { "TruckOrderAssignmentId", "AssignedAt", "IsAssigned", "OrderId", "TruckId", "UnassignedAt" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "O1-5a7bad59-2e44-4614-89c7-18679c6cc074", "T1-e0a1a249-fbda-4c74-b4e0-c9d8d8c83261", null });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Name",
                table: "Areas",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AreaId",
                table: "Locations",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FromLocId",
                table: "Orders",
                column: "FromLocId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ToLocId",
                table: "Orders",
                column: "ToLocId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TruckOrderAssignments_OrderId",
                table: "TruckOrderAssignments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckOrderAssignments_TruckId",
                table: "TruckOrderAssignments",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckUsers_TruckId",
                table: "TruckUsers",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckUsers_UserId",
                table: "TruckUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_CurrentAreaId",
                table: "Trucks",
                column: "CurrentAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TruckOrderAssignments");

            migrationBuilder.DropTable(
                name: "TruckUsers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
