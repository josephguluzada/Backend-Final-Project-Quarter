using Microsoft.AspNetCore.Mvc;
using Quarter.Models;
using Quarter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ContactViewModel contactVM = new ContactViewModel
            {
                ContactUsItems = _context.ContactUsItems.OrderBy(x => x.Order).ToList()
            };
            return View(contactVM);
        }
    }
}
