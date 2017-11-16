using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    public class InboxViewModelDTO
    {
      [Key]
      public int MessageID {get;set;}

      public int UserID { get; set; }

      [Display(Name = "Sender's First Name ")]
      public string FirstName { get; set; }

      [Display(Name = "Sender's Last Name")]
      public string LastName { get; set; }
        
      [Display(Name="For Request ID")]
      public int RequestID { get; set; }

      [Display(Name = "Mesage from Sender")]
      public string MessageMail { get; set; } 
    }
}