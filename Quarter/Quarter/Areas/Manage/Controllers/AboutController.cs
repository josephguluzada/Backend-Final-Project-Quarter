using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles =("SuperAdmin,Admin"))]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<About> abouts = _context.Abouts.OrderBy(x => x.Order).ToList();
            return View(abouts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(About about)
        {
            if (!ModelState.IsValid) return View();

            _context.Abouts.Add(about);
            _context.SaveChanges();

            return RedirectToAction("index","about");
        }

        public IActionResult Edit(int id)
        {
            About about = _context.Abouts.FirstOrDefault(x => x.Id == id);
            if (about == null) return NotFound();

            return View(about);
        }
        [HttpPost]
        public IActionResult Edit(About about)
        {
            About existAbout = _context.Abouts.FirstOrDefault(x => x.Id == about.Id);
            if (existAbout == null) return NotFound();
            if (!ModelState.IsValid) return View();

            existAbout.Title = about.Title;
            existAbout.Order = about.Order;
            existAbout.Icon = about.Icon;

            _context.SaveChanges();
            return RedirectToAction("index","about");
        }

        public IActionResult DeleteFetch(int id)
        {
            About about = _context.Abouts.FirstOrDefault(x => x.Id == id);
            if (about == null) return Json(new { status = 404 });

            try
            {
                _context.Abouts.Remove(about);
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
