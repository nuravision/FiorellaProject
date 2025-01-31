using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorella.Migrations
{
    public partial class createBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Description", "Image", "Title" },
                values: new object[] { 1, new DateTime(2025, 1, 24, 11, 1, 48, 739, DateTimeKind.Local).AddTicks(7510), "Description1", "blog-feature-img-1.jpg", "Blog title1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Description", "Image", "Title" },
                values: new object[] { 2, new DateTime(2025, 1, 24, 11, 1, 48, 739, DateTimeKind.Local).AddTicks(7531), "Description2", "blog-feature-img-3.jpg", "Blog title2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Description", "Image", "Title" },
                values: new object[] { 3, new DateTime(2025, 1, 24, 11, 1, 48, 739, DateTimeKind.Local).AddTicks(7533), "Description3", "blog-feature-img-4.jpg", "Blog title3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
