using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SaleManagerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SaleManagerController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<SaleManager> saleManagers = _context.SaleManagers.Include(x=>x.Products).ToList();

            return View(saleManagers);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SaleManager saleManager)
        {
            if (!ModelState.IsValid) return View();

            if(saleManager.ImageFile != null)
            {
                if (saleManager.ImageFile.ContentType != "image/jpeg" && saleManager.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Image file can be only jpg,jpeg or png!");
                    return View();
                }

                if (saleManager.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image file size can't be bigger than 2mb!");
                    return View();
                }

                string fileName = saleManager.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }
                string newFileName = Guid.NewGuid().ToString() + fileName;

                string path = Path.Combine(_env.WebRootPath, "uploads/salemanager", newFileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    saleManager.ImageFile.CopyTo(stream);
                }
                saleManager.Image = newFileName;
            }

            _context.SaleManagers.Add(saleManager);
            _context.SaveChanges();

            return RedirectToAction("index", "salemanager");

        }

        public IActionResult Edit(int id)
        {
            SaleManager saleManager = _context.SaleManagers.FirstOrDefault(x => x.Id == id);
            if (saleManager == null) return NotFound();
            return View(saleManager);
        }

        [HttpPost]
        public IActionResult Edit(SaleManager saleManager)
        {
            SaleManager _existSaleManager = _context.SaleManagers.FirstOrDefault(x => x.Id == saleManager.Id);
            if (_existSaleManager == null) return NotFound();
            if (!ModelState.IsValid) return View();

            if (saleManager.ImageFile != null)
            {

                if (saleManager.ImageFile.ContentType != "image/jpeg" && saleManager.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Image file can be only jpg,jpeg or png!");
                    return View();
                }

                if (saleManager.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image file size can't be bigger than 2mb!");
                    return View();
                }

                string fileName = saleManager.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }
                string newFileName = Guid.NewGuid().ToString() + fileName;

                string path = Path.Combine(_env.WebRootPath, "uploads/salemanager", newFileName);

                if (_existSaleManager.Image != null)
                {
                    string deletePath = Path.Combine(_env.WebRootPath, "uploads/salemanager", _existSaleManager.Image);
                    if (System.IO.File.Exists(deletePath))
                    {
                        System.IO.File.Delete(deletePath);
                    }
                }

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    saleManager.ImageFile.CopyTo(stream);
                }
                _existSaleManager.Image = newFileName;
            }
            else
            {
                if (saleManager.Image == null)
                {
                    if (_existSaleManager.Image != null)
                    {
                        string deletePath = Path.Combine(_env.WebRootPath, "uploads/slider", _existSaleManager.Image);
                        if (System.IO.File.Exists(deletePath))
                        {
                            System.IO.File.Delete(deletePath);
                        }
                    }
                    _existSaleManager.Image = null;
                }
            }

            _existSaleManager.FullName = saleManager.FullName;
            _existSaleManager.FacebookUrl = saleManager.FacebookUrl;
            _existSaleManager.TwitterUrl = saleManager.TwitterUrl;
            _existSaleManager.LinkedInUrl = saleManager.LinkedInUrl;
            _existSaleManager.Order = saleManager.Order;

            _context.SaveChanges();
            return RedirectToAction("index", "salemanager");
        }

        public IActionResult DeleteFetch(int id)
        {
            SaleManager saleManager= _context.SaleManagers.FirstOrDefault(x => x.Id == id);
            if (saleManager == null) return Json(new { status = 404 });

            try
            {
                _context.SaleManagers.Remove(saleManager);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return Json(new { status = 500 });
            }
            string deletePath = Path.Combine(_env.WebRootPath, "uploads/salemanager", saleManager.Image);
            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }

            return Json(new { status = 200 });
        }
    }
}
