using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            //AppUser admin = new AppUser
            //{
            //    UserName = "SuperAdmin",
            //    FullName = "Super Admin"
            //};

            //var result = await _userManager.CreateAsync(admin, "Admin123");
            //List<string> errors = new List<string>();
            //if (!result.Succeeded)
            //{
            //    foreach (var err in result.Errors)
            //    {
            //        errors.Add(err.Description);
            //    }
            //    return Ok(errors);
            //}

            IdentityRole role1 = new IdentityRole("SuperAdmin");
            await _roleManager.CreateAsync(role1);
            IdentityRole role2 = new IdentityRole("Admin");
            await _roleManager.CreateAsync(role2);
            IdentityRole role3 = new IdentityRole("Member");
            await _roleManager.CreateAsync(role3);

            return View();
        }
    }
}
