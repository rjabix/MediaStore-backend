using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaStore_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSmartphonesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Smartphones",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    Storage = table.Column<string>(type: "TEXT", nullable: true),
                    Display = table.Column<string>(type: "TEXT", nullable: true),
                    Resolution = table.Column<string>(type: "TEXT", nullable: true),
                    Camera = table.Column<string>(type: "TEXT", nullable: true),
                    Battery = table.Column<string>(type: "TEXT", nullable: true),
                    ScreenSize = table.Column<float>(type: "REAL", nullable: true),
                    Connectivity = table.Column<string>(type: "TEXT", nullable: true),
                    Features = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<string>(type: "TEXT", nullable: true),
                    Material = table.Column<string>(type: "TEXT", nullable: true),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<float>(type: "REAL", nullable: false),
                    rating = table.Column<int>(type: "INTEGER", nullable: true),
                    reviews = table.Column<int>(type: "INTEGER", nullable: true),
                    specialTags = table.Column<string>(type: "TEXT", nullable: true),
                    link = table.Column<string>(type: "TEXT", nullable: false),
                    oldprice = table.Column<int>(type: "INTEGER", nullable: true),
                    image = table.Column<string>(type: "TEXT", nullable: false),
                    category = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smartphones", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Smartphones");
        }
    }
}
