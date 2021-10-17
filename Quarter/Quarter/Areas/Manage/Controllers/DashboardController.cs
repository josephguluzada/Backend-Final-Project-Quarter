using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quarter.Areas.Manage.ViewModels;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles ="Admin,SuperAdmin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel
            {
                Orders = _context.Orders.ToList(),
                Reviews = _context.Reviews.Where(x => !x.IsAccepted).ToList()
            };
            return View(dashboardVM);
        }
    }
}
