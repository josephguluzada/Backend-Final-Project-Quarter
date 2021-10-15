using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public List<Review> Reviews { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Text { get; set; }
        public int Rate { get; set; }


    }
}
