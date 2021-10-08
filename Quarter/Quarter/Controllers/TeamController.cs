using Microsoft.AspNetCore.Mvc;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<SaleManager> saleManagers = _context.SaleManagers.OrderBy(x => x.Order).ToList();
            return View(saleManagers);
        }
    }
}
