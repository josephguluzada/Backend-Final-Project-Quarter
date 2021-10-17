using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quarter.Models;
using Quarter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(AppDbContext context,IHttpContextAccessor contextAccessor,UserManager<AppUser> userManager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public Setting GetSetting()
        {
            return _context.Settings.FirstOrDefault();
        }

        public List<WishListViewModel> GetWishItems()
        {
            AppUser member = null;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == _contextAccessor.HttpContext.User.Identity.Name && !x.IsAdmin);
            }

            List<WishListViewModel> wishListVM = new List<WishListViewModel>();

            if(member != null)
            {
                List<WishListItem> wishListItems = _context.WishListItems.Include(x => x.Product).ThenInclude(x => x.ProductImages).Where(x => x.AppUserId == member.Id).ToList();

                wishListVM = wishListItems.Select(x => new WishListViewModel
                {
                    ProductId = x.ProductId,
                    Image = x.Product.ProductImages.FirstOrDefault(pi => pi.IsPoster == true)?.Image,
                    Name = x.Product.Name,
                    Price = x.Product.SalePrice
                }).ToList();

            }

            return wishListVM;
        }
    }
}
