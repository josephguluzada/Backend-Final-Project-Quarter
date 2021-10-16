using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quarter.Areas.Manage.ViewModels
{
    public class AdminViewModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "Username must be min:5, max:50")]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 10, ErrorMessage = "Fullname must be min:10, max:50")]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Email must be max:100")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 5, ErrorMessage = "Password must be min:5, max:25")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 5, ErrorMessage = "Password must be min:5, max:25")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
