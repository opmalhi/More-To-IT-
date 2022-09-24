using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FYP.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kindly Enter a User Name?")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password Should Be Minimum Of 8 Character")]
        public string Password { get; set; }
    }
}