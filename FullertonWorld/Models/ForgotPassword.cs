using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    public class ForgotPassword
    {
        [Required(ErrorMessage="Enter your email to recover the password")]
        public string Email { get; set; }
    }
}