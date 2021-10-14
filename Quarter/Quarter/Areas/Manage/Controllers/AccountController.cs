﻿using Microsoft.AspNetCore.Identity;
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
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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

            //IdentityRole role1 = new IdentityRole("SuperAdmin");
            //await _roleManager.CreateAsync(role1);
            //IdentityRole role2 = new IdentityRole("Admin");
            //await _roleManager.CreateAsync(role2);
            //IdentityRole role3 = new IdentityRole("Member");
            //await _roleManager.CreateAsync(role3);

            AppUser appUser = await _userManager.FindByNameAsync("SuperAdmin");
            await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser admin = _userManager.Users.FirstOrDefault(x => x.NormalizedUserName == loginVM.UserName.ToUpper() && x.IsAdmin);
            if (admin == null)
            {
                ModelState.AddModelError("", "Username or password is not valid!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(admin, loginVM.Password, loginVM.IsPersistent, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is not valid!");
                return View();
            }

            return RedirectToAction("index","salemanager");
        }

    }
}
