using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    [Table("dbo.BuySellDatabase")]
    public class BuySellDatabase
    {
        [Key]
        [Display(Name = "Request ID")]
        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "*First Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Please enter the message.")]
        public string MessageMail { get; set; }

        [Display(Name = "Buy Or Sell")]
        [Required(ErrorMessage = "*Select the option")]
        public string BuyOrSell { get; set; }

        [Display(Name = "Item")]
        [Required(ErrorMessage = "Enter the Item")]
        public string Item { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Enter the price in digits")]
        public int Price { get; set; }

        [Display(Name = "Posted On")]
        [DataType(DataType.DateTime)]
        public DateTime? Requested_Date { get; set; }

        public int UserID { get; set; }

    }
}