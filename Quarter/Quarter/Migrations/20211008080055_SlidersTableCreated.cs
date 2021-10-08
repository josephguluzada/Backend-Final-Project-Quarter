using Microsoft.EntityFrameworkCore.Migrations;

namespace Quarter.Migrations
{
    public partial class SlidersTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title1 = table.Column<string>(maxLength: 30, nullable: false),
                    Title2 = table.Column<string>(maxLength: 30, nullable: true),
                    Desc = table.Column<string>(maxLength: 200, nullable: true),
                    SubTitle = table.Column<string>(maxLength: 20, nullable: true),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true),
                    RedirectUrlText = table.Column<string>(maxLength: 30, nullable: true),
                    RedirectUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
