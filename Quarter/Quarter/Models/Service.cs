using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class Service
    {
        public int Id { get; set; }
        [StringLength (maximumLength:50)]
        [Required]
        public string Title { get; set; }
        [StringLength(maximumLength: 300)]
        public string Desc { get; set; }
        [StringLength(maximumLength: 100)]
        public string Icon { get; set; }
        [StringLength(maximumLength: 30)]
        public string UrlText { get; set; }
        public int Order { get; set; }
    }
}
