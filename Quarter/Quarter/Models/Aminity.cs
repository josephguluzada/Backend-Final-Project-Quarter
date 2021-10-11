using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class Aminity
    {
        public int Id { get; set; }
        [StringLength(maximumLength:40)]
        public string Name { get; set; }
        [StringLength(maximumLength: 60)]
        public string Icon { get; set; }

        public List<ProductAminity> ProductAminities { get; set; }
    }
}
