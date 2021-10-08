using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class ContactUsItem
    {
        public int Id { get; set; }
        [StringLength (maximumLength:50)]
        public string Title { get; set; }
        [StringLength(maximumLength: 100)]
        public string Detail1 { get; set; }
        [StringLength(maximumLength: 100)]
        public string Detail2 { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
    }
}
