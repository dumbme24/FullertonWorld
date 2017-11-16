using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    public class UserLogin
    {
        [Key]
        public int UseId { get; set; }

        [Required(ErrorMessage="Email is required.")]
        public string userEmail { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage="Password is required.")]
        public string userPassword { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastLoginTime { get; set; }

    }
}