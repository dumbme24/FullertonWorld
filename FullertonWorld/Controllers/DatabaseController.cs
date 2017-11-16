using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationLogin.Models;
using System.Data.Entity;
using RegistrationLogin.Controllers;
using System.Dynamic;
using System.Web.Routing;

namespace RegistrationLogin.Controllers
{
    public class DatabaseController : Controller
    {
        //Get
        public ActionResult RoomateRequest()
        {
            return View();
        }

        //Get
        public ActionResult BuySellRequest()
        {
            return View();
        }

        // database of all requests
        public ActionResult RoomateDatabaseGrid()
        {
            using (AccountDbContext db = new AccountDbContext())
            {
                return View(db.rommateDatabase.ToList());
            }
        }

        // database of all requests
        public ActionResult sellbuygrid()
        {
            using (AccountDbContext db = new AccountDbContext())
            {
                return View(db.buySellDatabase.ToList());
            }
        }


        //Get request for romemate finder
        public ActionResult EditRequest(int id = 0)
        {
            AccountDbContext db = new AccountDbContext();
            RoommateDatabase user = db.rommateDatabase.Single(us => us.ID == id);
            return View(user);
        }

        [HttpPost]
        [ActionName("EditRequest")] // for roommate finder
        public ActionResult EditRequest_post(int id)
        {
            AccountDbContext db = new AccountDbContext();
            RoommateDatabase user = db.rommateDatabase.Single(us => us.ID == id);
            if (ModelState.IsValid)
            {
                UpdateModel(user, new string[] { "Name", "SelectOptionForPartner", "PartnerGender", "Location", "MessageMail", "Requested_Date", "UserID" });
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.MessageReq = "Details saved successfully.";
            }

            return View(user);
        }


        //Get request for SellBuy
        public ActionResult EditSellBuyRequest(int id = 0)
        {
            AccountDbContext db = new AccountDbContext();
            BuySellDatabase user = db.buySellDatabase.Single(us => us.ID == id);
            return View(user);
        }

        [HttpPost]
        [ActionName("EditSellBuyRequest")] // for SellBuy
        public ActionResult EditSellBuyRequest_Post(int id)
        {
            AccountDbContext db = new AccountDbContext();
            BuySellDatabase user = db.buySellDatabase.Single(us => us.ID == id);
            if (ModelState.IsValid)
            {
                UpdateModel(user, new string[] { "Name", "BuyOrSell", "Item", "Price", "MessageMail", "Requested_Date", "UserID" });
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.MessageReq = "Details saved successfully.";
            }

            return View(user);
        }


        // Users request details (Preview)
        public ActionResult RequestDetails(int id = 0)
        {
            AccountDbContext db = new AccountDbContext();
            RoommateDatabase user = db.rommateDatabase.Single(us => us.ID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // Users request details (Preview for BuySell Request)
        public ActionResult RequestBuySellDetails(int id = 0)
        {
            AccountDbContext db = new AccountDbContext();
            BuySellDatabase user = db.buySellDatabase.Single(us => us.ID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // Get // We are using 2 models // for roommate finder
        [HttpGet]
        public ActionResult ContactRequester(int id = 0)
        {
            AccountDbContext db = new AccountDbContext();
            RoommateDatabase user = db.rommateDatabase.Single(us => us.ID == id);
            if (user == null)
            {
                return HttpNotFound();  
            }
            SecondUserSendMessage abc = new SecondUserSendMessage();
            abc.roommateDB = user;
            abc.sendMessage = new SendMessage();
            return View("ContactRequester", abc);  
        }

        // Get // We are using 2 models
        [HttpGet]
        public ActionResult ContactBuySellRequester(int id = 0)
        {
            AccountDbContext db = new AccountDbContext();
            BuySellDatabase user = db.buySellDatabase.Single(us => us.ID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            SecondUserSendMessage abc = new SecondUserSendMessage();
            abc.buySellDB = user;
            abc.sendMessage = new SendMessage();
            return View("ContactBuySellRequester", abc);
        }



        //Send message to roommate requester
        [HttpPost]
        public ActionResult ContactRequester(SecondUserSendMessage twoModelClass)
         {
            {
                Message msg = new Message();
                string message = twoModelClass.sendMessage.MessageToRequester;
                int FromUserID = 0;
                FromUserID = Convert.ToInt32(Session["UserId"].ToString());
                int RequestID = 0;
                int ToUserID = 0;

                RequestID = twoModelClass.roommateDB.ID;
                ToUserID = twoModelClass.roommateDB.UserID;

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
                return View();
        }


        //Send message to BuySell Requester
        [HttpPost]
        public ActionResult ContactBuySellRequester(SecondUserSendMessage twoModelClass)
        {
            {
                Message msg = new Message();
                string message = twoModelClass.sendMessage.MessageToRequester;
                int FromUserID = 0;
                FromUserID = Convert.ToInt32(Session["UserId"].ToString());
                int RequestID = 0;
                int ToUserID = 0;

                RequestID = twoModelClass.buySellDB.ID;
                ToUserID = twoModelClass.buySellDB.UserID;

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
            return View();
        }


        // GET: /User/Delete/for Roommate finder
        public ActionResult DeleteRequest(int id = 0)
        {
            AccountDbContext db = new AccountDbContext();
            RoommateDatabase user = db.rommateDatabase.Single(us => us.ID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5

        [HttpPost]
        [ActionName("DeleteRequest")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            AccountDbContext db = new AccountDbContext();
            RoommateDatabase user = db.rommateDatabase.Single(us => us.ID == id);
            db.rommateDatabase.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UserRequestsForRoommate", "Database");
        }

        //Get request For buy-sell
        [HttpGet]
        public ActionResult DeleteBuySellRequest(int id = 0)
        {
            AccountDbContext db = new AccountDbContext();
            BuySellDatabase user = db.buySellDatabase.Single(us => us.ID == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ActionName("DeleteBuySellRequest")]
        public ActionResult DeleteBuySellRequest_Confirmed(int id = 0)
        {
            AccountDbContext db = new AccountDbContext();
            BuySellDatabase user = db.buySellDatabase.Single(us => us.ID == id);
            db.buySellDatabase.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UserRequestsForBuySell", "Database");
        }




        protected override void Dispose(bool disposing)
        {
            AccountDbContext db = new AccountDbContext();
            db.Dispose();
            base.Dispose(disposing);
        }

        //Get list of requests for current user
        public ActionResult UserRequestsForRoommate()
        {
            int currentUserID = 0;
            currentUserID = Convert.ToInt32(Session["UserId"].ToString());
            AccountDbContext db = new AccountDbContext();
            List<RoommateDatabase> RoommateRequests = db.rommateDatabase.Where(userReq => userReq.UserID == currentUserID).ToList();
            return View(RoommateRequests); 
        }


        //Get list of requests for current user
        public ActionResult UserRequestsForBuySell()
        {
            int currentUserID = 0;
            currentUserID = Convert.ToInt32(Session["UserId"].ToString());
            AccountDbContext db = new AccountDbContext();
            List<BuySellDatabase> BuySellRequests = db.buySellDatabase.Where(userReq => userReq.UserID == currentUserID).ToList();
            return View(BuySellRequests);
        }


        [HttpPost]
//      public ActionResult RoomateRequest(RoommateDatabase roommateDatabase)
        public ActionResult RoomateRequest(FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                RoommateDatabase rd = new RoommateDatabase();
                rd.Name = formCollection["Name"];
                rd.PartnerGender = formCollection["PartnerGender"];
                rd.SelectOptionForPartner = formCollection["SelectOptionForPartner"];
                rd.Location = formCollection["Location"];
                rd.MessageMail = formCollection["MessageMail"];
                rd.Requested_Date = Convert.ToDateTime(formCollection["Requested_Date"]);
                
                int currentUserID = 0;
                currentUserID = Convert.ToInt32(Session["UserId"].ToString());
                rd.UserID = currentUserID;

                AccountDbContext db = new AccountDbContext();
                db.rommateDatabase.Add(rd);
                db.SaveChanges();
                ViewBag.MessageReq = rd.Name + " you have successfully posted a new request.";
                ModelState.Clear();
                // SendActivationEmail(account.UserId); // Send email for query
            }
            return View();
        }


        [HttpPost]
        public ActionResult BuySellRequest(FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                BuySellDatabase buySellDB = new BuySellDatabase();

                buySellDB.Name = formCollection["Name"];
                buySellDB.BuyOrSell = formCollection["BuyOrSell"];
                buySellDB.Item = formCollection["Item"];
                buySellDB.MessageMail = formCollection["MessageMail"];
                buySellDB.Price = Convert.ToInt32(formCollection["Price"]);
                buySellDB.Requested_Date = Convert.ToDateTime(formCollection["Requested_Date"]);

                int currentUserID = 0;
                currentUserID = Convert.ToInt32(Session["UserId"].ToString());
                buySellDB.UserID = currentUserID;

                AccountDbContext db = new AccountDbContext();
                db.buySellDatabase.Add(buySellDB);
                db.SaveChanges();
                ViewBag.MessageReq = buySellDB.Name + " you have successfully posted a new request.";
                ModelState.Clear();
                // SendActivationEmail(account.UserId); // Send email for query
            }
            return View();
        }

        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                TempData["LoginMessage"] = "You need to login to access this service.";
                 return RedirectToAction("Login","Account");
            }            
        }
    }
}
