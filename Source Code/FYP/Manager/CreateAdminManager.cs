using FYP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FYP.Manager
{
    public class CreateAdminManager
    {
        int adminid = 0;
        int userid = 0;
        public int AddAdmin(CreateAdminModel aid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                Admin tbladmin = new Admin();
                tbladmin.Full_Name = aid.Full_Name;
                tbladmin.Father_Name = aid.Father_Name;
                tbladmin.NIC = aid.NIC;
                tbladmin.Gender = aid.Gender;
                tbladmin.DOB = aid.DOB;
                tbladmin.Contact_Number = aid.Contact_Number;
                tbladmin.Address = aid.Address;
                tbladmin.City = aid.City;
                tbladmin.State = aid.State;
                tbladmin.UserName = aid.UserName;
                tbladmin.Password = aid.Password;
                //tbladmin.Picture = aid.Picture;
                tbladmin.UserRole = aid.UserRole;
                tbladmin.IsActive = true;
                db.Admins.Add(tbladmin);
                db.SaveChanges();

                adminid = tbladmin.User_ID;
            }
            return adminid;
        }

        public int AddUser(UserModel uid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                User u = new User();
                u.Name = uid.Name;
                u.CNIC = uid.CNIC;
                u.Address = uid.Address;
                u.City = uid.City;
                u.State = uid.State;
                u.DOB = uid.DOB;
                u.Contact_Number = uid.Contact_Number;
                u.Email = uid.Email;
                u.Gender = uid.Gender;
                u.Password = uid.Password;
                u.UserRole = uid.UserRole;
                u.IsActive = true;
                db.Users.Add(u);
                db.SaveChanges();

                userid = u.U_Id;
            }
            return userid;
        }

        public List<CreateAdminModel> selectAdmin()
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Admins.ToList();
                List<CreateAdminModel> List = request.Select(x => new CreateAdminModel
                {
                    User_ID = x.User_ID,
                    //Picture = x.Picture,
                    Full_Name = x.Full_Name,
                    Father_Name = x.Father_Name,
                    NIC = x.NIC,
                    Gender = x.Gender,
                    DOB = x.DOB,
                    Contact_Number = x.Contact_Number,
                    Address = x.Address,
                    City = x.City,
                    State = x.State,
                    UserName = x.UserName,
                    Password = x.Password,
                    IsActive = x.IsActive.Value
                }).ToList();
                return List;
            }
        }

        public CreateAdminModel GetAdmin(int uid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Admins.Where(x => x.User_ID == uid).FirstOrDefault();
                CreateAdminModel Admin = null;
                if (request != null)
                {
                    Admin = new CreateAdminModel()
                    {
                        User_ID = request.User_ID,
                        //Picture = request.Picture,
                        Full_Name = request.Full_Name,
                        Father_Name = request.Father_Name,
                        NIC = request.NIC,
                        Gender = request.Gender,
                        DOB = request.DOB,
                        Contact_Number = request.Contact_Number,
                        Address = request.Address,
                        City = request.City,
                        State = request.State,
                        UserName = request.UserName,
                        Password = request.Password,
                        UserRole = request.UserRole.Value,
                    };
                    return Admin;
                }

                else
                {
                    return Admin;
                }
            }
        }

        public bool UpdateAdmin(CreateAdminModel uid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var Data = db.Admins.Where(x => x.User_ID == uid.User_ID).FirstOrDefault();
                if (Data != null)
                {
                    Data.User_ID = uid.User_ID;
                    //Data.Picture = uid.Picture;
                    Data.Full_Name = uid.Full_Name;
                    Data.Father_Name = uid.Father_Name;
                    Data.NIC = uid.NIC;
                    Data.Gender = uid.Gender;
                    Data.DOB = uid.DOB;
                    Data.Contact_Number = uid.Contact_Number;
                    Data.Address = uid.Address;
                    Data.City = uid.City;
                    Data.State = uid.State;
                    Data.UserName = uid.UserName;
                    Data.Password = uid.Password;
                    db.Entry(Data).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        public UserModel GetUser(int uid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Users.Where(x => x.U_Id == uid).FirstOrDefault();
                UserModel User = null;
                if (request != null)
                {
                    User = new UserModel()
                    {
                        U_Id = request.U_Id,
                        //Picture = request.Picture,
                        Name = request.Name,
                        CNIC = request.CNIC,
                        DOB = request.DOB,
                        Contact_Number = request.Contact_Number,
                        Address = request.Address,
                        City = request.City,
                        State = request.State,
                        Email = request.Email,
                        Password = request.Password,
                    };
                    return User;
                }

                else
                {
                    return User;
                }
            }
        }

        public bool UpdateUser(UserModel uid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var Data = db.Users.Where(x => x.U_Id == uid.U_Id).FirstOrDefault();
                if (Data != null)
                {
                    Data.U_Id = uid.U_Id;
                    //Data.Picture = uid.Picture;
                    Data.Name = uid.Name;
                    Data.CNIC = uid.CNIC;
                    Data.DOB = uid.DOB;
                    Data.Contact_Number = uid.Contact_Number;
                    Data.Address = uid.Address;
                    Data.City = uid.City;
                    Data.State = uid.State;
                    Data.Email = uid.Email;
                    Data.Password = uid.Password;
                    db.Entry(Data).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //public Booking GetPackDetail(int uid, int packid)
        //{
        //    using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
        //    {
        //        var user = db.Users.Where(x => x.U_Id == uid).FirstOrDefault();
        //        var pack = db.Packages.Where(x => x.Package_ID == packid).FirstOrDefault();
        //        Booking booking = null;
        //        if (user != null)
        //        {
        //            booking = new Booking()
        //            {
        //                U_Id = user.U_Id,
        //                Name = user.Name,
        //                CNIC = user.CNIC,
        //                DOB = user.DOB,
        //                Contact_Number = user.Contact_Number,
        //                Address = user.Address,
        //                City = user.City,
        //                State = user.State,
        //            };
        //            return booking;
        //        }

        //        else
        //        {
        //            return booking;
        //        }
        //    }
        //}
    }
}