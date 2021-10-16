using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    [Area("manage")]
    public class RoleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(AppDbContext context,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            List<IdentityRole> roles = _context.Roles.ToList();
            return View(roles);
        }
        [Authorize(Roles ="SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole identityRole)
        {
            if (!ModelState.IsValid) return View();

            await _roleManager.CreateAsync(identityRole);
            await _roleManager.UpdateAsync(identityRole);

            return RedirectToAction("index", "role");
        }


    }
}
