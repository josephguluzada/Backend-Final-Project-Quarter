using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class ProductAminity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AminityId { get; set; }

        public Product Product { get; set; }
        public Aminity Aminity { get; set; }
    }
}
