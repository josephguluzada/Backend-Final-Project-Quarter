using Quarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.ViewModels
{
    public class ServiceViewModel
    {
        public List<Service> Services { get; set; }
        public Setting Settings { get; set; }
    }
}
