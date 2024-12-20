﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ninjas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gold = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ninjas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoldValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    Agility = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    NinjaId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => new { x.NinjaId, x.EquipmentId });
                    table.ForeignKey(
                        name: "FK_Inventories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventories_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventories_Ninjas_NinjaId",
                        column: x => x.NinjaId,
                        principalTable: "Ninjas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NinjaId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shops_Ninjas_NinjaId",
                        column: x => x.NinjaId,
                        principalTable: "Ninjas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Head" },
                    { 2, "Chest" },
                    { 3, "Hand" },
                    { 4, "Feet" },
                    { 5, "Ring" },
                    { 6, "Necklace" }
                });

            migrationBuilder.InsertData(
                table: "Ninjas",
                columns: new[] { "Id", "Gold", "Name" },
                values: new object[,]
                {
                    { 1, 120m, "Ryu" },
                    { 2, 180m, "Ken" },
                    { 3, 90m, "Sakura" },
                    { 4, 200m, "Hayabusa" }
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "Agility", "CategoryId", "GoldValue", "Intelligence", "Name", "Strength" },
                values: new object[,]
                {
                    { 1, 2, 1, 50m, 0, "Samurai Helmet", 5 },
                    { 2, 5, 1, 40m, 0, "Hood of Shadows", 2 },
                    { 3, 1, 1, 60m, 8, "Mystic Crown", 0 },
                    { 4, -1, 2, 100m, 0, "Dragon Armor", 15 },
                    { 5, 3, 2, 30m, 0, "Leather Vest", 5 },
                    { 6, 0, 2, 50m, 10, "Mage Robe", 2 },
                    { 7, 1, 3, 25m, 0, "Iron Gauntlets", 8 },
                    { 8, 7, 3, 45m, 0, "Gloves of Dexterity", 3 },
                    { 9, 2, 3, 35m, 6, "Sorcerer's Gloves", 1 },
                    { 10, -1, 4, 40m, 0, "Steel Boots", 7 },
                    { 11, 9, 4, 55m, 0, "Ninja Tabi", 2 },
                    { 12, 3, 4, 30m, 4, "Boots of Insight", 1 },
                    { 13, 0, 5, 70m, 0, "Ring of Power", 10 },
                    { 14, 1, 5, 65m, 10, "Ring of Wisdom", 0 },
                    { 15, 8, 5, 45m, 0, "Agility Band", 1 },
                    { 16, -2, 6, 90m, 0, "Necklace of Fortitude", 12 },
                    { 17, 2, 6, 80m, 12, "Pendant of Wisdom", 0 },
                    { 18, 10, 6, 70m, 0, "Charm of Agility", 1 }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "EquipmentId", "NinjaId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 4, 1, 2 },
                    { 8, 2, 3 },
                    { 12, 3, 4 },
                    { 15, 4, 5 }
                });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "EquipmentId", "IsAvailable", "NinjaId" },
                values: new object[,]
                {
                    { 1, 1, false, 1 },
                    { 2, 4, false, 1 },
                    { 3, 8, false, 2 },
                    { 4, 12, false, 3 },
                    { 5, 15, false, 4 },
                    { 6, 2, true, 1 },
                    { 7, 5, true, 2 },
                    { 8, 9, true, 3 },
                    { 9, 14, true, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_CategoryId",
                table: "Equipments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CategoryId",
                table: "Inventories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_EquipmentId",
                table: "Inventories",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_NinjaId_EquipmentId_CategoryId",
                table: "Inventories",
                columns: new[] { "NinjaId", "EquipmentId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shops_EquipmentId",
                table: "Shops",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_NinjaId",
                table: "Shops",
                column: "NinjaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Ninjas");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
