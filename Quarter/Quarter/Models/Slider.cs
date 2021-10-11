using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 30)]
        [Required]
        public string Title1 { get; set; }
        [StringLength(maximumLength: 30)]
        public string Title2 { get; set; }
        [StringLength(maximumLength: 200)]
        public string Desc { get; set; }
        [StringLength(maximumLength: 20)]
        public string SubTitle { get; set; }
        [StringLength(maximumLength: 50)]
        public string Icon { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string RedirectUrlText { get; set; }
        [StringLength(maximumLength: 200)]
        public string RedirectUrl { get; set; }
        public int Order { get; set; }
    }
}
