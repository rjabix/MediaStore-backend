using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaStore_backend.Migrations
{
    /// <inheritdoc />
    public partial class Description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "description",
                table: "Smartphones",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "description",
                table: "Laptops",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Smartphones");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Laptops");
        }
    }
}
