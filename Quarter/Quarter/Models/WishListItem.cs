using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class WishListItem
    {
        public int Id { get; set; }
        public int Count { get; set; }

        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
