using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WiseSwitch.Migrations
{
    /// <inheritdoc />
    public partial class AddTutorialAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    SubModelId = table.Column<int>(type: "int", nullable: false),
                    TutorialId = table.Column<int>(type: "int", nullable: false),
                    ProceduresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tutorials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reset = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteStartupConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteConfigFiles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteVlanFiles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfigPorts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletePortsConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutorials", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tutorials");
        }
    }
}
