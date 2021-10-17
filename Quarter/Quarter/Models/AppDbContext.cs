using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SaleManager> SaleManagers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<ContactUsItem> ContactUsItems { get; set; }
        public DbSet<Aminity> Aminities { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SaleStatus> SaleStatuses { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAminity> ProductAminities { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }
    }
}
