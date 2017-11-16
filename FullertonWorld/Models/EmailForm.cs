using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    public class EmailForm
    {
            [Required(ErrorMessage="*Please enter your name"), Display(Name = "Your name")]
            public string FromName { get; set; }

            [Required(ErrorMessage = "*Please enter your email."), Display(Name = "Your email"), EmailAddress]
            public string FromEmail { get; set; }

            [Required(ErrorMessage = "*Please enter your message.")]
            [DataType(DataType.MultilineText)]
            public string Message { get; set; }
        }
    }
