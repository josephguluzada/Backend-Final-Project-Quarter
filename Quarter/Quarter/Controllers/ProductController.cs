using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quarter.Models;
using Quarter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            List<Product> products = _context.Products.
                              Include(x => x.SaleManager).
                              Include(x => x.ProductImages).
                              Include(x => x.SaleStatus).
                              Include(x => x.Category).
                              Include(x => x.City).
                              Include(x => x.ProductAminities).ThenInclude(x => x.Aminity).ToList();
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            ViewBag.Categories = _context.Categories.Include(x=>x.Products).ToList();
            List<Review> reviews = _context.Reviews.Include(x=>x.AppUser).Where(x => x.IsAccepted && x.ProductId == id).ToList();
            Order order = _context.Orders.Include(x => x.AppUser).FirstOrDefault(x=>x.Product.Id == id);

            Product product = _context.Products.
                              Include(x => x.SaleManager).
                              Include(x => x.ProductImages).
                              Include(x => x.SaleStatus).
                              Include(x => x.Category).
                              Include(x => x.City).
                              Include(x => x.ProductAminities).ThenInclude(x => x.Aminity).
                              FirstOrDefault(x => x.Id == id);

            ViewBag.SaleHouses = _context.Products.Where(x=>x.SaleStatus.Name == "Sale").Take(2).Include(x => x.SaleManager).
                              Include(x => x.ProductImages).
                              Include(x => x.SaleStatus).
                              Include(x => x.Category).
                              Include(x => x.City).
                              Include(x => x.ProductAminities).ThenInclude(x => x.Aminity).ToList();

            ViewBag.RentHouses = _context.Products.Where(x => x.SaleStatus.Name == "Rent").Take(2).Include(x => x.SaleManager).
                              Include(x => x.ProductImages).
                              Include(x => x.SaleStatus).
                              Include(x => x.Category).
                              Include(x => x.City).
                              Include(x => x.ProductAminities).ThenInclude(x => x.Aminity).ToList();

            ProductViewModel productVM = new ProductViewModel
            {
                Product =product,
                Reviews= reviews,
                Order = order
            };
           
            return View(productVM);
        }

        [Authorize(Roles ="Member")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(int id,ProductViewModel model)
        {
            if (!ModelState.IsValid) return View();
            if(model.Text.Length > 600)
            {
                ModelState.AddModelError("Text", "Text must be fewer than 600 characters");
                return RedirectToAction("index","error");
            }
            Product product = _context.Products.Find(id);
            if (product == null) return NotFound();
            AppUser member =await _userManager.FindByNameAsync(User.Identity.Name);
            Review review = new Review
            {
                AppUserId = member.Id,
                ProductId = id,
                Rate = model.Rate,
                Text = model.Text,
                CreatedAt = DateTime.UtcNow
                

            };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }
    }
}
