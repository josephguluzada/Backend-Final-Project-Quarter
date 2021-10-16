using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quarter.Models;
using Quarter.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.Controllers
{
    [Authorize(Roles ="SuperAdmin,Admin")]
    [Area("manage")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public OrderController(AppDbContext context,IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            List<Order> orders = _context.Orders.OrderByDescending(x=>x.CreatedAt).Include(x => x.Product).Include(x => x.AppUser).ToList();
            return View(orders);
        }

        public IActionResult Edit(int id)
        {
            Order order = _context.Orders.Include(x => x.Product).Include(x => x.AppUser).FirstOrDefault(x => x.Id == id);
            if (order == null) return NotFound();
            return View(order);
        }

        public IActionResult Accept(int id)
        {
            Order order = _context.Orders.Include(x=>x.AppUser).Include(x=>x.Product).FirstOrDefault(x => x.Id == id);

            if (order == null) return NotFound();

            order.Status = Models.Enums.OrderStatus.Accepted;

            _context.SaveChanges();

            string body = string.Empty;
            using(StreamReader reader = new StreamReader("wwwroot/templates/order.html"))
            {
                body = reader.ReadToEnd();
            }

            string tr = $@"<tr>
                                <td width=\""75 % \"" align=\""left\"" style=\""font - family: Open Sans, Helvetica, Arial, sans-serif; font - size: 16px; font - weight: 400; line - height: 24px; padding: 15px 10px 5px 10px; \"" > {order.Product.Name} </td>
                                <td width=\""25 % \"" align=\""left\"" style=\""font - family: Open Sans, Helvetica, Arial, sans-serif; font - size: 16px; font - weight: 400; line - height: 24px; padding: 15px 10px 5px 10px; \"" > ${order.Product.SalePrice} </td>
                                                  </tr>";
            string orderHtml = string.Empty;
            orderHtml += tr;
            body = body.Replace("{{price}}", order.Price.ToString()).Replace("{{order}}",orderHtml);

            _emailService.Send(order.AppUser.Email, "Congratulations! Your order accepted!",body);
            return RedirectToAction("index");
        }

        public IActionResult Reject(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);

            if (order == null) return NotFound();

            order.Status = Models.Enums.OrderStatus.Rejected;

            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
