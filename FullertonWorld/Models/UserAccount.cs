using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*Email is required.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter a valid Email.")]
        public string Email { get; set; }

        [StringLength(20,MinimumLength=8,ErrorMessage="Passowrd must be in between minimun 8 characters and maximum 20 characters.")]
        [Required(ErrorMessage = "*Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "*Please confirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }
    }
}