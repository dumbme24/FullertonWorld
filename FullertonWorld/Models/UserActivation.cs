using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    [Table("dbo.UserActivation")]
    public class UserActivation
    {
        [Key]
        // This attribute to solve the issue to insert values in database explicitely (null value exception)
        [DatabaseGenerated(DatabaseGeneratedOption.None)]  
        public int ID { get; set; }
        
        public string ActivationCode { get; set; }
    }
}