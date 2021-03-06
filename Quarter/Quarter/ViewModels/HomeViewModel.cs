using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Service> Services { get; set; }
        public Setting Settings { get; set; }
        public List<About> Abouts { get; set; }
        public List<Aminity> Aminities { get; set; }
        public List<Product> Products { get; set; }
        public Product LastSoldProduct { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
