using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class SaleStatus
    {
        public int Id { get; set; }
        [StringLength (maximumLength:50)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
