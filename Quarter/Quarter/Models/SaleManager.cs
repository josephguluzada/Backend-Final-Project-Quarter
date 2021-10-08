﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class SaleManager
    {
        public int Id { get; set; }
        [StringLength (maximumLength:50)]
        [Required]
        public string FullName { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        public int Order { get; set; }
        [StringLength(maximumLength: 150)]
        public string FacebookUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string TwitterUrl { get; set; }
        [StringLength(maximumLength: 150)]
        public string LinkedInUrl { get; set; }



    }
}
