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
            if (!_context.Cities.Any(x => x.Id == product.CityId)) ModelState.AddModelError("CityId", "City not found!");
            if (!_context.Categories.Any(x => x.Id == product.CategoryId)) ModelState.AddModelError("CategoryId", "Category not found!");
            if (!_context.SaleManagers.Any(x => x.Id == product.SaleManagerId)) ModelState.AddModelError("SaleManagerId", "Sale Manager not found!");
            if (!_context.SaleStatuses.Any(x => x.Id == product.SaleStatusId)) ModelState.AddModelError("SaleStatusId", "Sale Status not found!");

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

            if(product.ImageFiles != null)
            {
                foreach (var file in product.ImageFiles)
                {
                    if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
                    {
                        continue;
                    }
                    if (file.Length > 2097152)
                    {
                        continue;
                    }

                    ProductImage productImage = new ProductImage
                    {
                        IsPoster = false,
                        Image = FileManager.Save(_env.WebRootPath, "uploads/product", file)
                    };

                    if (product.ProductImages == null)
                        product.ProductImages = new List<ProductImage>();
                    product.ProductImages.Add(productImage);
                }
            }

            if (!ModelState.IsValid) return View();


            _context.Products.Add(product);
            _context.SaveChanges();


            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Product product = _context.Products.Include(x => x.Category)
                                                      .Include(x => x.City)
                                                      .Include(x => x.SaleManager)
                                                      .Include(x => x.SaleStatus)
                                                      .Include(x => x.ProductAminities).ThenInclude(x => x.Aminity)
                                                      .Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);
            ViewBag.Cities = _context.Cities.ToList();
            ViewBag.SaleManagers = _context.SaleManagers.ToList();
            ViewBag.Aminities = _context.Aminities.ToList();
            ViewBag.SaleStatus = _context.SaleStatuses.ToList();
            ViewBag.Categories = _context.Categories.ToList();

            product.AminityIds = product.ProductAminities.Select(x => x.AminityId).ToList();
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!_context.Cities.Any(x => x.Id == product.CityId)) ModelState.AddModelError("CityId", "City not found!");
            if (!_context.Categories.Any(x => x.Id == product.CategoryId)) ModelState.AddModelError("CategoryId", "Category not found!");
            if (!_context.SaleManagers.Any(x => x.Id == product.SaleManagerId)) ModelState.AddModelError("SaleManagerId", "Sale Manager not found!");
            if (!_context.SaleStatuses.Any(x => x.Id == product.SaleStatusId)) ModelState.AddModelError("SaleStatusId", "Sale Status not found!");

            if (!ModelState.IsValid) return View();
            Product existProduct = _context.Products.Include(x => x.Category)
                                                      .Include(x => x.City)
                                                      .Include(x => x.SaleManager)
                                                      .Include(x => x.SaleStatus)
                                                      .Include(x => x.ProductAminities).ThenInclude(x => x.Aminity)
                                                      .Include(x => x.ProductImages).FirstOrDefault(x => x.Id == product.Id);

            if (existProduct == null) return NotFound();

            if (product.PosterImage != null)
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

                ProductImage posterImage = _context.ProductImages.FirstOrDefault(x => x.IsPoster == true);
                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/product", product.PosterImage);

                if (posterImage == null)
                {
                    posterImage = new ProductImage
                    {
                        IsPoster = true,
                        Image = newFileName,
                        ProductId = product.Id
                    };

                    _context.ProductImages.Add(posterImage);
                }
                else
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/product", posterImage.Image);
                    posterImage.Image = newFileName;
                }
            }

            existProduct.ProductAminities.RemoveAll((x => !product.AminityIds.Contains(x.AminityId)));

            if (product.AminityIds != null)
            {
                foreach (var aminityId in product.AminityIds.Where(x => !existProduct.ProductAminities.Any(at => at.AminityId == x)))
                {
                    ProductAminity productAminity = new ProductAminity
                    {
                        AminityId = aminityId,
                        ProductId = product.Id
                    };
                    if (existProduct.ProductAminities == null)
                        existProduct.ProductAminities = new List<ProductAminity>();
                    existProduct.ProductAminities.Add(productAminity);
                }
            }

            existProduct.ProductImages.RemoveAll(x => x.IsPoster == false && !product.ImageFilesIds.Contains(x.Id));

            if (product.ImageFiles != null)
            {
                foreach (var file in product.ImageFiles)
                    {
                        if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                        {
                            continue;
                        }

                        if (file.Length > 2097152)
                        {
                            continue;
                        }

                        ProductImage image = new ProductImage
                        {
                            IsPoster = false,
                            Image = FileManager.Save(_env.WebRootPath, "uploads/product", file)
                        };
                    if (existProduct.ProductImages == null)
                        existProduct.ProductImages = new List<ProductImage>();
                    existProduct.ProductImages.Add(image);
                }
            }



            existProduct.Name = product.Name;
            existProduct.AreaSize = product.AreaSize;
            existProduct.BathCount = product.BathCount;
            existProduct.BedCount = product.BedCount;
            existProduct.CategoryId = product.CategoryId;
            existProduct.CityId = product.CityId;
            existProduct.CreatedAt = product.CreatedAt;
            existProduct.Desc = product.Desc;
            existProduct.IsFeature = product.IsFeature;
            existProduct.Location = product.Location;
            existProduct.ParkingCount = product.ParkingCount;
            existProduct.ProductFloor = product.ProductFloor;
            existProduct.ProductFloorCount = product.ProductFloorCount;
            existProduct.Rate = product.Rate;
            existProduct.RoomCount = product.RoomCount;
            existProduct.SaleManagerId = product.SaleManagerId;
            existProduct.SalePrice = product.SalePrice;
            existProduct.SaleStatusId = product.SaleStatusId;


            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult DeleteFetch(int id)
        {
            Product product = _context.Products.Include(x=>x.ProductImages).FirstOrDefault(x => x.Id == id);
            if (product == null) return Json(new { status = 404 });


            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                List<ProductImage> images = product.ProductImages.ToList();
                foreach (var image in images)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/product", image.Image);
                }
            }
            catch (Exception)
            {

                return Json(new { status = 500 });
            }



            return Json(new { status = 200 });
        }
    }
}
