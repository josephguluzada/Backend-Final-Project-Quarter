using Microsoft.EntityFrameworkCore.Migrations;

namespace Quarter.Migrations
{
    public partial class SaleManagersTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaleManagers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    FacebookUrl = table.Column<string>(maxLength: 150, nullable: true),
                    TwitterUrl = table.Column<string>(maxLength: 150, nullable: true),
                    LinkedInUrl = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleManagers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleManagers");
        }
    }
}
