using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.ViewModels
{
    public class AboutViewModel
    {
        public Setting Settings { get; set; }
        public List<About> Abouts { get; set; }
        public List<Service> Services { get; set; }
        public List<SaleManager> SaleManagers { get; set; }
    }
}
