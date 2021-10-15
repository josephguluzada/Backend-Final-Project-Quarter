using Quarter.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        [StringLength(maximumLength: 50)]
        public string FullName { get; set; }
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
        [StringLength(maximumLength: 30)]
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength(maximumLength: 200)]
        public string Adress { get; set; }
        public OrderStatus Status { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
