using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Service> services = _context.Services.OrderBy(x => x.Order).ToList();
            return View(services);
        }

        public IActionResult Create()
        {
            ViewBag.Icons = _context.Services.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid) return View();

            _context.Services.Add(service);
            _context.SaveChanges();

            return RedirectToAction("index", "service");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Icons = _context.Services.Select(x => new SelectListItem {Value = x.Icon,Text=x.Icon }).Distinct().ToList();
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);
            if (service == null) return NotFound();

            return View(service);
        }
        [HttpPost]
        public IActionResult Edit(Service service)
        {
            Service existService = _context.Services.FirstOrDefault(x => x.Id == service.Id);
            if (existService == null) return NotFound();
            if (!ModelState.IsValid) return View();

            existService.Title = service.Title;
            existService.Order = service.Order;
            existService.UrlText = service.UrlText;
            existService.Icon = service.Icon;
            existService.Desc = service.Desc;

            _context.SaveChanges();
            return RedirectToAction("index", "service");
        }

        public IActionResult DeleteFetch(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);
            if (service == null) return Json(new { status = 404 });

            try
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return Json(new { status = 500 });
            }

            return Json(new { status = 200 });
        }
    }
}
