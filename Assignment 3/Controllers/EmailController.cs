using Assignment_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Assignment_3.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Compose()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compose(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                // Configure and send the email
                using (var message = new MailMessage())
                {
                    message.To.Add(model.ToEmail);
                    if (!string.IsNullOrWhiteSpace(model.CCEmail))
                        message.CC.Add(model.CCEmail);
                    if (!string.IsNullOrWhiteSpace(model.BCCEmail))
                        message.Bcc.Add(model.BCCEmail);

                    message.Subject = model.Subject;
                    message.Body = model.Body;
                    message.IsBodyHtml = true;

                    //using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                    //{
                    //    smtpClient.Credentials = new NetworkCredential("vedantbharad26@gmail.com", "rotc zagv oedh tdpf");
                    //    smtpClient.Send(message);
                    //}
                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                    {
                        smtpClient.Port = 587; // Port for Gmail's SMTP server
                        smtpClient.Credentials = new NetworkCredential("mbmbmb2312@gmail.com", "your password");
                        smtpClient.EnableSsl = true; // Enable SSL for secure connection

                        // Send the email
                        smtpClient.Send(message);

                        Console.WriteLine("Email sent successfully.");
                    }
                }

                ViewBag.SuccessMessage = "Email sent successfully!";
                ModelState.Clear(); // Clear form inputs
            }

            return View(model);
        }
    }
}