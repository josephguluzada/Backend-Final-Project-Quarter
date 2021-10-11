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

        public ProductController(AppDbContext context)
        {
            _context = context;
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
            Product product = _context.Products.
                              Include(x => x.SaleManager).
                              Include(x => x.ProductImages).
                              Include(x => x.SaleStatus).
                              Include(x => x.Category).
                              Include(x => x.City).
                              Include(x => x.ProductAminities).ThenInclude(x => x.Aminity).
                              FirstOrDefault(x => x.Id == id);
           
            return View(product);
        }
    }
}
