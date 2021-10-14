using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.ViewModels
{
    public class MemberRegisterViewModel
    {
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Username must be min:5, max:50")]
        public string UserName { get; set; }
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string FullName { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


    }
}
