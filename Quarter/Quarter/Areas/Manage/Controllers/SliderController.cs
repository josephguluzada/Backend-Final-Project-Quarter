using Microsoft.AspNetCore.Mvc;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index", "slider");
        }

        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (existSlider == null) return NotFound();

            existSlider.Icon = slider.Icon;
            existSlider.Order = slider.Order;
            existSlider.RedirectUrl = slider.RedirectUrl;
            existSlider.RedirectUrlText = slider.RedirectUrlText;
            existSlider.SubTitle = slider.SubTitle;
            existSlider.Title1 = slider.Title1;
            existSlider.Title2 = slider.Title2;
            existSlider.Desc = slider.Desc;

            _context.SaveChanges();
            return RedirectToAction("index", "slider");
        }
    }
}
