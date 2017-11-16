using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationLogin.Models;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Data.Entity;


namespace RegistrationLogin.Controllers
{
    public class AccountController : Controller
    {
        public int USERID = 0;

        // This is to display all the registered users list // only for admin
        public ActionResult Index()
        {
            using (AccountDbContext db = new AccountDbContext())
            {
                return View(db.userAccounts.ToList());
            }
        }

        public ActionResult Register()
        {
            string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
            //        TempData["notice"] = activationCode;

            if (activationCode != "00000000-0000-0000-0000-000000000000")
            {
                using (var db = new AccountDbContext())
                {

                    try
                    {
                        var query_D = (from b in db.userActivation
                                       where b.ActivationCode == activationCode
                                       select b).First();
                        query_D.ActivationCode = "00000000-0000-0000-0000-000000000000";
                        db.Entry(query_D).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch
                    {
                        return RedirectToAction("Login", "Account");
                    }

                }
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }


        //Email Confirmation Method
        [HttpGet]
        public ActionResult UserActivation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserActivation(UserActivation ua)
        {
            int userId = ua.ID;
          //  SendActivationEmail(userId);
            return View();
        }


        private void SendActivationEmail(int userId)
        {
            string activationCode = Guid.NewGuid().ToString();
            using (AccountDbContext db = new AccountDbContext())
            {
                // Here first check if the activation code is already present (sent first time to the user) the retrive the same activation code from th table and send email again.
                    UserActivation ua = new UserActivation();

               //     ua = db.userActivation.FirstOrDefault(userAct => userAct.ID == userId); 
                    
                 var checkUser = (from row in db.userActivation
                                  where row.ID == userId
                                  select row).ToList();

                 if (checkUser.Count() == 0)
                 {
                     // Here add activation code and UserId in activation table
                     ua.ID = userId;
                     ua.ActivationCode = activationCode;
                     db.Entry(ua).State = EntityState.Added;
                     db.SaveChanges();
                 }
                 else                 
                 {
                     activationCode = checkUser[0].ActivationCode;
                 }
            }

                AccountDbContext dbContext = new AccountDbContext();
                UserAccount user = dbContext.userAccounts.FirstOrDefault(u => u.UserId == userId);
                string toEmailID = user.Email;
                string userName = user.FirstName;
                MailMessage mm = new MailMessage("fullertonexplorer@gmail.com", toEmailID);
                mm.Subject = "Account Activation";
                string body = "Hello " + userName;
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = '" + Request.Url.AbsoluteUri + "?ActivationCode=" + activationCode + "'>Click here to activate your account.</a>";


                body += "<br /><br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("fullertonexplorer@gmail.com", "");
                smtp.UseDefaultCredentials = false;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = NetworkCred;
                smtp.Port = 25;
                smtp.Send(mm);
            
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {

            if (ModelState.IsValid)
            {
                using (AccountDbContext db = new AccountDbContext())
                {
                    var checkUser = (from row in db.userAccounts
                                     where row.Email == account.Email
                                     select row).ToList();

                    if (checkUser.Count() == 0)
                    {
                        db.userAccounts.Add(account);
                        db.SaveChanges();
                        ViewBag.Message = account.FirstName + " " + account.LastName + " Successfully registered.";
                        SendActivationEmail(account.UserId);
                    }
                    else
                    {
                        ViewBag.Message = account.Email + " already registered.";
                    }
                }

                ModelState.Clear();
            }
            return View();
        }

        //Login get request
        public ActionResult Login()
        {
            if (Session["UserId"] != null)
            {
                return RedirectToAction("Index", "Main");
            }

            else  if (TempData["LoginMessage"] != null)
            {
                ViewBag.LoginMessage = TempData["LoginMessage"].ToString();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            if (string.IsNullOrEmpty(user.userEmail))
            {
                ModelState.AddModelError("firstName", "The Email field is required.");
            }

            if (string.IsNullOrEmpty(user.userPassword))
            {
                ModelState.AddModelError("lastName", "The Password field is required.");
            }
            else
            {
                using (AccountDbContext db = new AccountDbContext())
                {

                    var usrCheck = (from row in db.userAccounts
                                    where (row.Email == user.userEmail)
                                    select row).ToList();

                    if (usrCheck.Count == 0)
                    {
                        ModelState.AddModelError("", user.userEmail + " is not registered. Please register it to access the service.");
                        return View();
                    }

                    var usr = (from userRow in db.userAccounts
                               where (userRow.Email == user.userEmail && userRow.Password == user.userPassword)
                               select userRow).ToList();

                    if (usr.Count != 0)
                    {
                        int userid = usr[0].UserId;
                        var activationCode = (from id in db.userActivation
                                              where (id.ID == userid)
                                              select id).ToList();
                        ViewBag.ID = userid;

                        if (activationCode[0].ActivationCode == "00000000-0000-0000-0000-000000000000")
                        {
                            //Add 
                            Session["UserId"] = usr[0].UserId;
                            Session["Email"] = usr[0].Email.ToString();
                            Session["FirstName"] = usr[0].FirstName;
                            Session["user"] = usr[0];

                            if (Session["UserId"] != null)
                            {
                                return RedirectToAction("Index", "Main");
                            }
                        }
                        else
                        {
                            // Redirect to activation page
                            return RedirectToAction("UserActivation", "Account");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email or Password is wrong.");
                    }
                }
            }

            return View();
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session["UserId"] = null;
            return RedirectToAction("Login", "Account");
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword forgetPassword)
        {
            if (ModelState.IsValid)
            {
                using (AccountDbContext db = new AccountDbContext())
                {
                    var checkUser = (from row in db.userAccounts
                                     where row.Email == forgetPassword.Email
                                     select row).ToList();

                    if (checkUser.Count() != 0)
                    {
                        SendPasswordEmail(checkUser[0]);
                        ViewBag.Message = "Password sent to email id " + checkUser[0].Email;
                    }
                    else
                    {
                        ViewBag.Message = forgetPassword.Email + " not registered.";
                    }
                }
                ModelState.Clear();
            }
            return View();
        }

        public void SendPasswordEmail(UserAccount user)
        {
            using (MailMessage mm = new MailMessage("fullertonexplorer@gmail.com", user.Email.ToString()))
            {
                mm.Subject = "Password Recovery";
                string body = "Hello " + user.FirstName;
                body += "<br /><br />Your password for Fullerton Explorer account is :";
                body += user.Password;
                body += " <br/><br/> Please do not share it with anyone";
                body += "<br /><br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("fullertonexplorer@gmail.com", "Rockstar@fullerton");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }

        }

        [HttpGet]
        public ActionResult EditUserProfile()
        {
            int? currentUserID = 0;
            currentUserID = Convert.ToInt32(Session["UserId"].ToString());
            if (currentUserID != 0 || currentUserID != null)
            {
                AccountDbContext db = new AccountDbContext();
                UserAccount user = db.userAccounts.Single(us => us.UserId == currentUserID);
                return View(user);
            }
            return View();
        }

        [HttpPost]
        [ActionName("EditUserProfile")]
        public ActionResult EditUserProfile_Post()
        {
            int? currentUserID = 0;
            currentUserID = Convert.ToInt32(Session["UserId"].ToString());
            AccountDbContext db = new AccountDbContext();
            UserAccount user = db.userAccounts.Single(us => us.UserId == currentUserID);
            if (ModelState.IsValid)
            {
                UpdateModel(user, new string[] { "UserId", "FirstName", "LastName" });
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.SavedMessage = "Details updated successfuly!";
            }

            return View(user);
        }


        public ActionResult UserDetails()
        {
            int currentUserID = 0;
            currentUserID = Convert.ToInt32(Session["UserId"].ToString());

            AccountDbContext db = new AccountDbContext();
            UserAccount user = db.userAccounts.Single(us => us.UserId == currentUserID);
            return View(user);
        }
    }
}
