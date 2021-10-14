﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Models
{
    public class AppUser:IdentityUser
    {
        [StringLength(maximumLength:50)]
        public string FullName { get; set; }
    }
}
