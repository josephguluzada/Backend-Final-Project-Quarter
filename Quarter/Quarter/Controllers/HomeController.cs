using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quarter.Models;
using Quarter.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            List<Order> orders = _context.Orders.Where(x=>x.Status == Models.Enums.OrderStatus.Accepted).ToList();

            double sumOfAreas = 0;
            int sumOfRooms = 0;
            double sumOfAcceptedOrders = orders.Count();
            foreach (var item in products)
            {
                sumOfAreas += item.AreaSize;
                sumOfRooms += item.RoomCount;
            }

            

            ViewBag.Cities = _context.Cities.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.SaleStatuses = _context.SaleStatuses.ToList();
            ViewBag.SumOfAreas = sumOfAreas;
            ViewBag.SumOfAcceptedOrders = sumOfAcceptedOrders;
            ViewBag.SumOfRooms = sumOfRooms;

            HomeViewModel homeVM = new HomeViewModel
            {
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                Services = _context.Services.Skip(3).Take(3).ToList(),
                Settings = _context.Settings.FirstOrDefault(),
                Abouts = _context.Abouts.OrderBy(x => x.Order).ToList(),
                Aminities = _context.Aminities.ToList(),
                Products = _context.Products.Include(x => x.City).
                                             Include(x => x.SaleManager).
                                             Include(x => x.SaleStatus).
                                             Include(x => x.ProductImages).ToList(),
                LastSoldProduct = _context.Orders.OrderByDescending(x=>x.Id).FirstOrDefault().Product,
                Reviews = _context.Reviews.Include(x=>x.AppUser).ToList()
            };
            return View(homeVM);
        }



    }
}
