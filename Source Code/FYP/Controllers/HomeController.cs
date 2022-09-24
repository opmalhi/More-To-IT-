using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FYP.Filters;  

namespace FYP.Controllers
{
    [AuthorizedUser]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminLogout()
        {
            //FormsAuthentication.SignOut();
            //Session.Abandon();
            //Session.Clear();

            FormsAuthentication.SignOut();
            HttpCookie c = Request.Cookies[FormsAuthentication.FormsCookieName];
            c.Expires = DateTime.Now.AddDays(-1);
            // Update the amended cookie!
            Response.Cookies.Set(c);
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Login");
        }
        
        public ActionResult UserLogout()
        {
            //FormsAuthentication.SignOut();
            //Session.Abandon();
            //Session.Clear();
            
            FormsAuthentication.SignOut();
            HttpCookie c = Request.Cookies[FormsAuthentication.FormsCookieName];
            c.Expires = DateTime.Now.AddDays(-1);
            // Update the amended cookie!
            Response.Cookies.Set(c);
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Home", "User");
        }
    }
}