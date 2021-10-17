using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public HomeController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        public IActionResult AddToWishList(int id)
        {
            Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

            WishListViewModel wishListVM = null;

            if (product == null) return NotFound();

            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);

            }
            List<WishListViewModel> wishProduct = new List<WishListViewModel>();

            if (member != null)
            {
                WishListItem wishListItem = _context.WishListItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);

                if (wishListItem == null)
                {
                    wishListItem = new WishListItem
                    {
                        AppUserId = member.Id,
                        ProductId = id
                    };
                    _context.WishListItems.Add(wishListItem);
                }

                _context.SaveChanges();

                wishProduct = _context.WishListItems.Select(x =>
                                        new WishListViewModel
                                        {
                                            ProductId = x.ProductId,
                                            Name = x.Product.Name,
                                            Price = x.Product.SalePrice,
                                            Image = x.Product.ProductImages.FirstOrDefault(x => x.IsPoster == true).Image
                                        }).ToList();

            }

            return PartialView("_WishListPartial", wishProduct);
            
        }

        public IActionResult DeleteFromWishList(int id)
        {
            Product product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

            WishListViewModel wishListVM = null;

            AppUser member = null;

            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);

            }
            List<WishListViewModel> wishProduct = new List<WishListViewModel>();

            if (member != null)
            {
                WishListItem wishListItem = _context.WishListItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);

                _context.WishListItems.Remove(wishListItem);
                _context.SaveChanges();

                wishProduct = _context.WishListItems.Select(x =>
                                        new WishListViewModel
                                        {
                                            ProductId = x.ProductId,
                                            Name = x.Product.Name,
                                            Price = x.Product.SalePrice,
                                            Image = x.Product.ProductImages.FirstOrDefault(x => x.IsPoster == true).Image
                                        }).ToList();


            }

            return PartialView("_WishListPartial", wishProduct);
        }

    }
}
