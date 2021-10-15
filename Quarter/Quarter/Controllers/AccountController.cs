using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quarter.Models;
using Quarter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser member = await _userManager.FindByNameAsync(memberRegisterVM.UserName);

            if(member != null)
            {
                ModelState.AddModelError("UserName", "Username is already taken!");
                return View();
            }

            member = await _userManager.FindByEmailAsync(memberRegisterVM.Email);
            if(member != null)
            {
                ModelState.AddModelError("Email", "Email is already taken!");
                return View();
            }

            member = new AppUser
            {
                FullName = memberRegisterVM.FullName,
                UserName = memberRegisterVM.UserName,
                Email = memberRegisterVM.Email
            };

            var result = await _userManager.CreateAsync(member, memberRegisterVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(member, "Member");
            await _signInManager.SignInAsync(member, true);

            return RedirectToAction("index", "home");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MemberLoginViewModel memberLoginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser member = _userManager.Users.FirstOrDefault(x => x.NormalizedUserName == memberLoginVM.UserName.ToUpper());

            if(member == null)
            {
                ModelState.AddModelError("", "Username or password is not valid!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(member, memberLoginVM.Password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is not valid!");
                return View();
            }


            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }
        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Profile()
        {
            AppUser member = await _userManager.FindByNameAsync(User.Identity.Name);

            ProfileViewModel profileVM = new ProfileViewModel
            {
                UserName = member.UserName,
                FullName = member.FullName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                Orders = _context.Orders.Include(x => x.Product).Where(x => x.AppUserId == member.Id).ToList()
            };
            return View(profileVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Profile(ProfileViewModel profileVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser member = await _userManager.FindByNameAsync(User.Identity.Name);


            if (!string.IsNullOrWhiteSpace(profileVM.ConfirmNewPassword) && !string.IsNullOrWhiteSpace(profileVM.NewPassword))
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(member, profileVM.CurrentPassword, profileVM.NewPassword);

                if (!passwordChangeResult.Succeeded)
                {
                    foreach (var item in passwordChangeResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    return View();
                }

            }
            if (member.Email != profileVM.Email && _userManager.Users.Any(x => x.NormalizedEmail == profileVM.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "This email has already been taken!");
                return View();
            }

            member.FullName = profileVM.FullName;
            member.Email = profileVM.Email;
            member.PhoneNumber = profileVM.PhoneNumber;

            var result = await _userManager.UpdateAsync(member);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View();
            }

            return RedirectToAction("profile","account");
        }
    }
}
