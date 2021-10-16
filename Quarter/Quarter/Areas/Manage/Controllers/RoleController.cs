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
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if (!ModelState.IsValid) return View();

            await _roleManager.CreateAsync(role);
            await _roleManager.UpdateAsync(role);

            return RedirectToAction("index", "role");
        }
        [Authorize(Roles ="SuperAdmin")]
        public IActionResult Edit(string name)
        {
            IdentityRole role = _roleManager.Roles.FirstOrDefault(x => x.Name == name);
            if (role == null) return NotFound();
            TempData["name"] = name;

            return View(role);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(IdentityRole identityRole)
        {
            var name = TempData["name"];
            IdentityRole existRole = _roleManager.Roles.FirstOrDefault(x => x.Name == name.ToString());
            if (existRole == null) return NotFound();
            existRole.Name = identityRole.Name;

            await _roleManager.UpdateAsync(existRole);

            return RedirectToAction("index","role");
        }

        public async Task<IActionResult> Delete(string name)
        {

            IdentityRole deleteRole = _roleManager.Roles.FirstOrDefault(x => x.Name == name);
            await _roleManager.DeleteAsync(deleteRole);

            return RedirectToAction("index","role");
        }
    }
}
