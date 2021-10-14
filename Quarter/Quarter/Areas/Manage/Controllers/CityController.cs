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
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class CityController : Controller
    {
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<City> cities = _context.Cities.ToList();
            return View(cities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(City city)
        {
            if (!ModelState.IsValid) return View();

            _context.Cities.Add(city);
            _context.SaveChanges();

            return RedirectToAction("index", "city");
        }

        public IActionResult Edit(int id)
        {
            City city = _context.Cities.FirstOrDefault(x => x.Id == id);
            if (city == null) return NotFound();

            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(City city)
        {
            City existCity = _context.Cities.FirstOrDefault(x => x.Id == city.Id);
            if (existCity == null) return NotFound();
            if (!ModelState.IsValid) return View();

            existCity.Name = city.Name;

            _context.SaveChanges();

            return RedirectToAction("index", "city");
        }

        [Authorize(Roles ="Admin,SuperAdmin")]
        public IActionResult DeleteFetch(int id)
        {
            City city = _context.Cities.FirstOrDefault(x => x.Id == id);
            if (city == null) return Json(new { status = 404 });

            try
            {
                _context.Cities.Remove(city);
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
