using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    public class OutboxViewModelDTO
    {
        [Key]
        public int MessageID { get; set; }
        public int UserID { get; set; }

        [Display(Name = "Recipient's First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Recipient's Last Name")]
        public string LastName { get; set; }

        [Display(Name = "For Request ID")]
        public int RequestID { get; set; }

        [Display(Name = "Message to Recipient")]
        public string MessageMail { get; set; }
    }
}