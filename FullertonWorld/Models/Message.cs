using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    [Table("dbo.Message")]
    public class Message
    {
        public int FromUserID { get; set; }
        public int ToUserID { get; set; }
        public int RequestID { get; set; }
        public int MessageID { get; set; }
    
        [Display(Name="Message")]    
        public string MessageMail { get; set; }

    }
}