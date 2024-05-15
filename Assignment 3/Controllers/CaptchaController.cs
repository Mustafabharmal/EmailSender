using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using Assignment_3.Models;

namespace Assignment_3.Controllers
{
    public class CaptchaController : Controller
    {
        // GET: Captcha  
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Registration Registration)
        {
            // Code for validating the Captcha  
            if (!this.IsCaptchaValid(""))
            {
                ViewBag.ErrMessage = "Captcha is not valid";
                return View("Index", Registration);
            }
            return Content("Captcha is valid");
        }
    }
}