using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CQRS.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("4d8ecc60-c26c-4d76-a73a-aa4c5f6ebb04"), "Sony's top-of-the-line wireless noise-canceling headphones", "Sony WH-1000XM4", 349.99m },
                    { new Guid("8cb10811-d0a3-4b8c-b2ac-a9d08fd7272c"), "Dell's high-performance laptop with a 4K InfinityEdge display", "Dell XPS 15", 1899.99m },
                    { new Guid("b2825246-bf86-45b2-af9b-47f1a2aaa820"), "Apple's latest flagship smartphone with a ProMotion display and improved cameras", "iPhone 15 Pro", 999.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
