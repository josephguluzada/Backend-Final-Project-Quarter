using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Quarter.Helpers;
using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            Setting settings = _context.Settings.FirstOrDefault();
            return View(settings);
        }

        public IActionResult Edit()
        {
            Setting setting = _context.Settings.FirstOrDefault();
            return View(setting);
        }

        [HttpPost]
        public IActionResult Edit(Setting setting)
        {
            Setting existSetting = _context.Settings.FirstOrDefault();
            if (existSetting == null) return NotFound();

            if (!ModelState.IsValid) return View(existSetting);


            if(setting.HeaderImageFile != null)
            {
                if (setting.HeaderImageFile.ContentType != "image/jpeg" && setting.HeaderImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("HeaderImageFile", "Image type can be only .png or .jpeg");
                    return View();
                }
                if (setting.HeaderImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("HeaderImageFile", "Image size can not be bigger than 2mb");
                    return View();
                }
                if(existSetting.HeaderLogo != null)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", existSetting.HeaderLogo);
                }
                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", setting.HeaderImageFile);
                existSetting.HeaderLogo = newFileName;
            }
            else
            {
                if(setting.HeaderLogo == null)
                {
                    if(existSetting.HeaderLogo != null)
                    {
                        FileManager.Delete(_env.WebRootPath, "uploads/setting", existSetting.HeaderLogo);
                        existSetting.HeaderLogo = null;
                    }
                }
            }

            if (setting.FooterImageFile != null)
            {
                if (setting.FooterImageFile.ContentType != "image/jpeg" && setting.FooterImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("FooterImageFile", "Image type can be only .png or .jpeg");
                    return View();
                }
                if (setting.FooterImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("FooterImageFile", "Image size can not be bigger than 2mb");
                    return View();
                }
                if (existSetting.FooterLogo != null)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", existSetting.FooterLogo);
                }
                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", setting.FooterImageFile);
                existSetting.FooterLogo = newFileName;
            }
            else
            {
                if (setting.FooterLogo == null)
                {
                    if (existSetting.FooterLogo != null)
                    {
                        FileManager.Delete(_env.WebRootPath, "uploads/setting", existSetting.FooterLogo);
                        existSetting.FooterLogo = null;
                    }
                }
            }

            if (setting.AboutImageFile != null)
            {
                if (setting.AboutImageFile.ContentType != "image/jpeg" && setting.AboutImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("AboutImageFile", "Image type can be only .png or .jpeg");
                    return View();
                }
                if (setting.AboutImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("AboutImageFile", "Image size can not be bigger than 2mb");
                    return View();
                }
                if (existSetting.AboutImage != null)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", existSetting.AboutImage);
                }
                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", setting.AboutImageFile);
                existSetting.AboutImage = newFileName;
            }
            else
            {
                if (setting.AboutImage == null)
                {
                    if (existSetting.AboutImage != null)
                    {
                        FileManager.Delete(_env.WebRootPath, "uploads/setting", existSetting.AboutImage);
                        existSetting.AboutImage = null;
                    }
                }
            }

            if (setting.ServiceImageFile != null)
            {
                if (setting.ServiceImageFile.ContentType != "image/jpeg" && setting.ServiceImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ServiceImageFile", "Image type can be only .png or .jpeg");
                    return View();
                }
                if (setting.ServiceImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ServiceImageFile", "Image size can not be bigger than 2mb");
                    return View();
                }
                if (existSetting.ServiceImage != null)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", existSetting.ServiceImage);
                }
                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", setting.ServiceImageFile);
                existSetting.ServiceImage = newFileName;
            }
            else
            {
                if (setting.ServiceImage == null)
                {
                    if (existSetting.ServiceImage != null)
                    {
                        FileManager.Delete(_env.WebRootPath, "uploads/setting", existSetting.ServiceImage);
                        existSetting.ServiceImage = null;
                    }
                }
            }

            if (setting.HomePageVideoImageFile != null)
            {
                if (setting.HomePageVideoImageFile.ContentType != "image/jpeg" && setting.HomePageVideoImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ServiceImageFile", "Image type can be only .png or .jpeg");
                    return View();
                }
                if (setting.HomePageVideoImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ServiceImageFile", "Image size can not be bigger than 2mb");
                    return View();
                }
                if (existSetting.HomePageImage != null)
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", existSetting.HomePageImage);
                }
                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", setting.HomePageVideoImageFile);
                existSetting.HomePageImage = newFileName;
            }
            else
            {
                if (setting.HomePageImage == null)
                {
                    if (existSetting.HomePageImage != null)
                    {
                        FileManager.Delete(_env.WebRootPath, "uploads/setting", existSetting.HomePageImage);
                        existSetting.HomePageImage = null;
                    }
                }
            }

            existSetting.AboutDesc = setting.AboutDesc;
            existSetting.AboutTitle = setting.AboutTitle;
            existSetting.AboutUrl = setting.AboutUrl;
            existSetting.AboutUrlText = setting.AboutUrlText;
            existSetting.Adress = setting.Adress;
            existSetting.ContactMail = setting.ContactMail;
            existSetting.CopyRight = setting.CopyRight;
            existSetting.DribbleUrl = setting.DribbleUrl;
            existSetting.FacebookUrl = setting.FacebookUrl;
            existSetting.FooterDesc = setting.FooterDesc;
            existSetting.InstagramUrl = setting.InstagramUrl;
            existSetting.Phone = setting.Phone;
            existSetting.SupportMail = setting.SupportMail;
            existSetting.TwitterUrl = setting.TwitterUrl;

            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
