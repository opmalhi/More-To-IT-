using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Manager
{
    public class LoginManager
    {
        public CreateAdminModel checklogin(LoginModel logindata)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var data = db.Admins.Where(x => x.UserName == logindata.Email && x.Password == logindata.Password && x.UserRole == 1).FirstOrDefault();
                CreateAdminModel admindata = null;
                if (data != null)
                {
                    admindata = new CreateAdminModel()
                    {
                        Full_Name = data.Full_Name,
                        User_ID = data.User_ID,
                        Picture = data.Picture,
                        UserName = data.UserName,
                        Password = data.Password,
                        IsActive = data.IsActive.Value,
                    };
                }
                return admindata;
            }
        }
        
        public UserModel checkUserLogin(LoginModel logindata)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var data = db.Users.Where(x => x.Email == logindata.Email && x.Password == logindata.Password && x.UserRole == 2).FirstOrDefault();
                UserModel userdata = null;
                if (data != null)
                {
                    userdata = new UserModel()
                    {
                        Name = data.Name,
                        U_Id = data.U_Id,
                        Picture = data.Picture,
                        Email = data.Email,
                        Password = data.Password,
                        IsActive = data.IsActive.Value,
                    };
                }
                return userdata;
            }
        }

       
    }
}