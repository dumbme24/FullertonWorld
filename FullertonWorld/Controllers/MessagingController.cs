using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationLogin.Models;
using RegistrationLogin.Controllers;

namespace RegistrationLogin.Controllers
{
    public class MessagingController : Controller
    {
        public ActionResult Inbox()
        {
            IEnumerable<InboxViewModelDTO> model = null;

            AccountDbContext db = new AccountDbContext();
            int? currentUserID = 0;
            currentUserID = Convert.ToInt32(Session["UserId"]);
            
/*            model = (from a in db.rommateDatabase
                     join b in db.userAccounts on a.UserID equals b.UserId
                     join c in db.messaging on b.UserId equals c.FromUserID
                     where b.UserId == currentUserID
*/
                     model = (from a in db.messaging
                     join b in db.userAccounts on a.FromUserID equals b.UserId
                     join c in db.rommateDatabase on a.RequestID equals c.ID
                     where a.ToUserID == currentUserID

                     select new
                     {
                         MessageID = a.MessageID,
                         UserID = b.UserId,
                         FirstName = b.FirstName,
                         LastName = b.LastName,
                         RequestID = c.ID,
                         MessageMail = a.MessageMail
                     }).AsEnumerable().Select(x => new InboxViewModelDTO
                     {
                         MessageID = x.MessageID,
                         UserID = x.UserID,
                         FirstName = x.FirstName,
                         LastName = x.LastName,
                         RequestID = x.RequestID,
                         MessageMail = x.MessageMail
                     }).ToList();

            return View(model);
        }

        public ActionResult Outbox()
        {
            IEnumerable<OutboxViewModelDTO> model = null;

            AccountDbContext db = new AccountDbContext();
            int? currentUserID = 0;
            currentUserID = Convert.ToInt32(Session["UserId"]);

            model = (from a in db.messaging
                     join b in db.userAccounts on a.ToUserID equals b.UserId
                     join c in db.rommateDatabase on a.RequestID equals c.ID
                     where a.FromUserID == currentUserID
                     select new
                     {
                         UserID = b.UserId,
                         FirstName = b.FirstName,
                         LastName = b.LastName,
                         RequestID = c.ID,
                         MessageMail = a.MessageMail
                     }).AsEnumerable().Select(x => new OutboxViewModelDTO
                     {
                         UserID = x.UserID,
                         FirstName = x.FirstName,
                         LastName = x.LastName,
                         RequestID = x.RequestID,
                         MessageMail = x.MessageMail
                     }).ToList();

            return View(model);
        }




        // Reply to sender
        [HttpGet]
        public ActionResult ReplyBack(int id = 0)  // here goes messageID from inbox
        {
            AccountDbContext db = new AccountDbContext();
            Message msgDetails = db.messaging.Single(msg => msg.MessageID == id);
            if (msgDetails == null)
            {
                return HttpNotFound();
            }

            SecondUserReplyMessage abc = new SecondUserReplyMessage();
            // Here 
            abc.MessageDetails = msgDetails;
            abc.replyMessage = new ReplyMessage();
            return View("ReplyBack", abc);
        }

        // Reply to sender
        [HttpPost]
        public ActionResult ReplyBack(SecondUserReplyMessage twoModelClass)  // here goes messageID from inbox
        {
           {
                Message msg = new Message();
                string message = twoModelClass.replyMessage.MessageBack;
                int FromUserID = 0;
                FromUserID = Convert.ToInt32(Session["UserId"].ToString());
                int RequestID = 0;
                int ToUserID = 0;

                RequestID = twoModelClass.MessageDetails.RequestID;
                ToUserID = twoModelClass.MessageDetails.FromUserID;

                msg.FromUserID = FromUserID;
                msg.RequestID = RequestID;
                msg.ToUserID = ToUserID;
                msg.MessageMail = message;

                AccountDbContext db = new AccountDbContext();
                db.messaging.Add(msg);
                db.SaveChanges();
                ViewBag.MessageReq = "Message sent successfully.";
                ModelState.Clear();
                // Call the method to update the Inbox table

        }
           return View("ReplyBack");
        }

    }
}
