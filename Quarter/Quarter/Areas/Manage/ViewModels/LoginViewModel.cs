using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.ViewModels
{
    public class LoginViewModel
    {
        [StringLength(maximumLength:50,MinimumLength =5,ErrorMessage ="Username must be min:5, max:50")]
        public string UserName { get; set; }
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Password must be min:5, max:50")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; } = false;
    }
}
