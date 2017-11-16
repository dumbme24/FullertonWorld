using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using RegistrationLogin.Models;
using System.Threading.Tasks;

namespace RegistrationLogin.Controllers
{
    public class MainController : Controller
    {

        public ActionResult Index()
        {
            // These 3 lines for noCache to avoid the back button session issue
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            return View();

/*            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
 */ 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EmailForm model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("fullertonexplorer@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("fullertonexplorer@gmail.com");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "fullertonexplorer@gmail.com",  // replace with valid value
                        Password = "Rockstar@fullerton"  // replace with valid value
                    };
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    ViewBag.MessageSent = " Message sent successfully";
                    message.Dispose();
                    ModelState.Clear();

                }
            }
            return View();
        }
    }
}
