using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
    public class SecondUserSendMessage
    {
        public SendMessage sendMessage { get; set; }
        public RoommateDatabase roommateDB { get; set; }
        public BuySellDatabase buySellDB { get; set; }
    }

    public class SendMessage
    {
        public string MessageToRequester {get; set;}
        public int FromUserID { get; set; }
        public int ToUserID { get; set; }
        public int RequestID { get; set; }

    }
}