using Microsoft.AspNetCore.Mvc;
using Quarter.Models;
using Quarter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AboutViewModel aboutVM = new AboutViewModel
            {
                Settings = _context.Settings.FirstOrDefault(),
                Abouts = _context.Abouts.OrderBy(x => x.Order).ToList(),
                SaleManagers = _context.SaleManagers.Take(3).OrderBy(x => x.Order).ToList(),
                Services = _context.Services.Skip(3).Take(3).OrderBy(x => x.Order).ToList()
            };

            return View(aboutVM);
        }
    }
}
