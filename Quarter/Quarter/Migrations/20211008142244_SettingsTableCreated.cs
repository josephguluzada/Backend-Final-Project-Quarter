using Microsoft.EntityFrameworkCore.Migrations;

namespace Quarter.Migrations
{
    public partial class SettingsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderLogo = table.Column<string>(maxLength: 100, nullable: false),
                    FooterLogo = table.Column<string>(maxLength: 100, nullable: false),
                    FacebookIcon = table.Column<string>(maxLength: 100, nullable: true),
                    TwitterIcon = table.Column<string>(maxLength: 50, nullable: true),
                    InstagramIcon = table.Column<string>(maxLength: 50, nullable: true),
                    DribbleIcon = table.Column<string>(maxLength: 50, nullable: true),
                    LocationIcon = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneIcon = table.Column<string>(maxLength: 50, nullable: true),
                    MailIcon = table.Column<string>(maxLength: 50, nullable: true),
                    FooterDesc = table.Column<string>(maxLength: 250, nullable: true),
                    Adress = table.Column<string>(maxLength: 100, nullable: true),
                    ContactMail = table.Column<string>(maxLength: 50, nullable: true),
                    SupportMail = table.Column<string>(maxLength: 50, nullable: true),
                    AboutImage = table.Column<string>(maxLength: 150, nullable: true),
                    AboutTitle = table.Column<string>(maxLength: 50, nullable: true),
                    AboutDesc = table.Column<string>(maxLength: 250, nullable: true),
                    AboutSubDesc = table.Column<string>(maxLength: 150, nullable: true),
                    AboutUrlText = table.Column<string>(maxLength: 30, nullable: true),
                    AboutUrl = table.Column<string>(maxLength: 100, nullable: true),
                    CopyRight = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
