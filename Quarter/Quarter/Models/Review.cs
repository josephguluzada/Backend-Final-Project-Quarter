using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required,StringLength(250)]
        public string Text { get; set; }
        public int Rate { get; set; }
        public bool IsAccepted { get; set; } = false;
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
