using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP.Models;
using FYP.Manager;
using System.Web.Security;

namespace FYP.Controllers
{

    public class LoginController : Controller
    {
        MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel logindata)
        {
            if (ModelState.IsValid)
            {
                LoginManager obj = new LoginManager();
                CreateAdminModel admindata = obj.checklogin(logindata);
                if (admindata != null)
                {
                    if (admindata.IsActive == false)
                    {
                        TempData["Message"] = "You have currently disabled, kindly contact your Admin";
                        return View();
                    }
                    Session["IsLogedIn"] = true;
                    Session["Full_Name"] = admindata.Full_Name;
                    Session["User_ID"] = admindata.User_ID;
                    Session["UserRole"] = "admin";
                    if (!string.IsNullOrEmpty(admindata.Picture))
                    {
                        Session["UserImage"] = admindata.Picture;
                    }
                    else {
                        Session["UserImage"] = "https://imgs.search.brave.com/zXXzrLJOha0Enw8eUJBwbjBIm1DotMC8phHW2S_5jAc/rs:fit:474:225:1/g:ce/aHR0cHM6Ly90c2Ux/Lm1tLmJpbmcubmV0/L3RoP2lkPU9JUC5z/d2FZTVdBUzhjZmJ5/TUtMRWlRSjJRRDZE/NiZwaWQ9QXBp";
                    }
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    TempData["Message"] = "UserName Or Password Incorrect";
                    return View();
                }
            }
            else
            {
                TempData["Message"] = "Please Enter UserName and Password First";
                return View();
            }

        }

        [HttpPost]
        public ActionResult UserLogin(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var isExists = db.Users.Any(u => u.Email == user.Email);

                if (isExists)
                {
                    TempData["Message"] = "Email Already Exists!";

                }
                else { 
                    CreateAdminManager obj = new CreateAdminManager();
                    user.UserRole = 2;
                    int u_id = obj.AddUser(user);
                    if (u_id > 0)
                    {
                        TempData["Message"] = "User Created Successfully and User Id is " + u_id;

                        var body = "Dear " + user.Name + " ,\n\nThank you for registering to use our online services. \n\nRegards,\nMore To IT!";
                        EmailHelper.SendEmail(user.Email,"Confirm your account", body);
                        return RedirectToAction("UserLogin");
                    }

                    else
                    {
                        TempData["Message"] = "User Is Not Created!";
                    }
                }
            }
            else
            {
                TempData["Message"] = "User Not Created Kindly Fill Complete Form!";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLoginCheck(LoginModel logindata)
        {
            if (ModelState.IsValid)
            {
                LoginManager obj = new LoginManager();
                UserModel userdata = obj.checkUserLogin(logindata);
                if (userdata != null)
                {
                    if (userdata.IsActive == false)
                    {
                        TempData["Message"] = "You have currently disabled, kindly contact your Admin";
                        return RedirectToAction("UserLogin");
                    }
                    Session["IsLogedIn"] = true;
                    Session["Full_Name"] = userdata.Name;
                    Session["U_Id"] = userdata.U_Id;
                    if (!string.IsNullOrEmpty(userdata.Picture))
                    {
                        Session["UserImage"] = userdata.Picture;
                    }
                    else
                    {
                        Session["UserImage"] = "https://imgs.search.brave.com/zXXzrLJOha0Enw8eUJBwbjBIm1DotMC8phHW2S_5jAc/rs:fit:474:225:1/g:ce/aHR0cHM6Ly90c2Ux/Lm1tLmJpbmcubmV0/L3RoP2lkPU9JUC5z/d2FZTVdBUzhjZmJ5/TUtMRWlRSjJRRDZE/NiZwaWQ9QXBp";
                    }
                    FormsAuthentication.SetAuthCookie(logindata.Email, false);
                    return RedirectToAction("Home", "User");
                }
                else
                {
                    TempData["Message"] = "UserName Or Password Incorrect";
                    return RedirectToAction("UserLogin");
                }
            }
            else
            {
                TempData["Message"] = "Please Enter UserName and Password First";
                return RedirectToAction("UserLogin");
            }

        }
    }
}