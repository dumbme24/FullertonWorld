using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    [Table("dbo.RoommateDatabase")]
    public class RoommateDatabase
    {
        [Key]
        [Display(Name="Request ID")]
        public int ID { get; set; }

        [Display(Name="Name")]
        [Required(ErrorMessage = "*First Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Please enter the message.")]
        public string MessageMail { get; set; }

        [Display(Name = "Option")]
        [Required(ErrorMessage = "*Select the option")]
        public string SelectOptionForPartner { get; set; }

        [Display(Name = "Partner")]   
        [Required(ErrorMessage = "Select the partner gender")]
        public string PartnerGender { get; set; }

        [Display(Name = "Location")]   
        [Required(ErrorMessage = "Enter the location in text")]
        public string Location { get; set; }
        
        [Display(Name = "Posted On")]   
        [DataType(DataType.DateTime)]
        public DateTime? Requested_Date { get; set; }

        public int UserID { get; set; }

    }
}