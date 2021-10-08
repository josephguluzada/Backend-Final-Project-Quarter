using Microsoft.EntityFrameworkCore.Migrations;

namespace Quarter.Migrations
{
    public partial class SaleManagersImagesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "SaleManagers",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "SaleManagers");
        }
    }
}
