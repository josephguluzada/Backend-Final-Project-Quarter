using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Member")]
        public IActionResult Buy(int id)
        {
            if (!ModelState.IsValid) return View();
            Product product = _context.Products.Include(x => x.Category)
                                                      .Include(x => x.City)
                                                      .Include(x => x.SaleManager)
                                                      .Include(x => x.SaleStatus)
                                                      .Include(x => x.ProductAminities).ThenInclude(x => x.Aminity)
                                                      .Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.NormalizedUserName == User.Identity.Name.ToUpper() && !x.IsAdmin);
            }

            Order order = new Order
            {
                FullName = member.FullName,
                Price = product.SalePrice,
                PhoneNumber = member.PhoneNumber,
                Adress = product.Location,
                Email = member.Email,
                CreatedAt = DateTime.UtcNow,
                Status = Models.Enums.OrderStatus.Pending,
                AppUserId = member.Id,
                ProductId = product.Id
            };


            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("profile", "account");
        }
    }
}
