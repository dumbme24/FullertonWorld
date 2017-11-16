using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationLogin.Models
{
        public class SecondUserReplyMessage
        {
            public ReplyMessage replyMessage { get; set; }
            public Message MessageDetails { get; set; }
        }

        public class ReplyMessage
        {
            public string MessageBack { get; set; }
            public int FromUserID { get; set; }
            public int ToUserID { get; set; }
            public int RequestID { get; set; }
        }
}
