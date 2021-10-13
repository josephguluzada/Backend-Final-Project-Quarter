using Microsoft.EntityFrameworkCore.Migrations;

namespace Quarter.Migrations
{
    public partial class AboutImageNameChangedToServiceImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutImage2",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "ServiceImage",
                table: "Settings",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceImage",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "AboutImage2",
                table: "Settings",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);
        }
    }
}
