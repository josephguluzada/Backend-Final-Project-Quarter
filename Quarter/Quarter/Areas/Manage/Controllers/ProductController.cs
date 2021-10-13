using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quarter.Helpers;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Product> products = _context.Products.Include(x=>x.Category)
                                                      .Include(x=>x.City)
                                                      .Include(x=>x.SaleManager)
                                                      .Include(x=>x.SaleStatus)
                                                      .Include(x=>x.ProductAminities).ThenInclude(x=>x.Aminity)
                                                      .Include(x=>x.ProductImages).ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Cities = _context.Cities.ToList();
            ViewBag.SaleManagers = _context.SaleManagers.ToList();
            ViewBag.Aminities = _context.Aminities.ToList();
            ViewBag.SaleStatus = _context.SaleStatuses.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ProductAminity productAminity = null;
            foreach (var aminityId in product.AminityIds)
            {
                Aminity aminity = _context.Aminities.FirstOrDefault(x => x.Id == aminityId);

                if(aminity == null)
                {
                    ModelState.AddModelError("AminityIds", "Aminity not found!");
                    return View();
                }

                productAminity = new ProductAminity
                {
                    AminityId = aminityId,
                    ProductId = product.Id
                };
                if (product.ProductAminities == null)
                    product.ProductAminities = new List<ProductAminity>();
                product.ProductAminities.Add(productAminity);
            }

            product.ProductImages = new List<ProductImage>();

            if(product.PosterImage == null)
            {
                ModelState.AddModelError("PosterImage", "Poster image is required");
            }
            else
            {
                if (product.PosterImage.ContentType != "image/jpeg" && product.PosterImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("PosterImage", "Image type can be only .png or .jpeg");
                    return View();
                }
                if (product.PosterImage.Length > 2097152)
                {
                    ModelState.AddModelError("PosterImage", "Image size can not be bigger than 2mb");
                    return View();
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/product", product.PosterImage);

                ProductImage posterImage = new ProductImage
                {
                    Image = newFileName,
                    IsPoster = true,
                };

                product.ProductImages.Add(posterImage);
            }

            if (!ModelState.IsValid) return View();


            _context.Products.Add(product);
            _context.SaveChanges();


            return RedirectToAction("index");
        }
    }
}
