// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quarter.Models;

namespace Quarter.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211008101642_SaleManagersImagesAdded")]
    partial class SaleManagersImagesAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Quarter.Models.SaleManager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FacebookUrl")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LinkedInUrl")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("TwitterUrl")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("SaleManagers");
                });

            modelBuilder.Entity("Quarter.Models.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("RedirectUrl")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("RedirectUrlText")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("SubTitle")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Title1")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Title2")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });
#pragma warning restore 612, 618
        }
    }
}
