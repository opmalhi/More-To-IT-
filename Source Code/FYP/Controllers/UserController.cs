using FYP.Filters;
using FYP.Manager;
using FYP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ZXing;

namespace FYP.Controllers
{

    public class UserController : Controller
    {
        MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize]
        //public ActionResult Rating()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Rating(int packId)
        //{
        //    return View();
        //}

        //public ActionResult PackageReview()
        //{
        //    PackageReview review = new PackageReview();
        //    //review.Package_ID = packid;
        //    //int vehicleApplications = db.Vehicles.Count(x => x.Status == false);
        //    return PartialView("_PackageReview", review);
        //}

        //public ActionResult GuiderReview()
        //{
        //    //int vehicleApplications = db.Vehicles.Count(x => x.Status == false);
        //    return PartialView("_GuiderReview");
        //}
        //public ActionResult VehicleReview()
        //{
        //    //int vehicleApplications = db.Vehicles.Count(x => x.Status == false);
        //    return PartialView("_VehicleReview");
        //}

        public ActionResult Home()
        {
            ViewBag.City = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
            PackageManager obj = new PackageManager();
            List<AddPackageModel> package = obj.selectPackage();
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View(package);
        }
        
        public ActionResult Gallery()
        {
            return View();
        }

        public ActionResult ViewDetails(int id)
        {
            PackageManager obj = new PackageManager();
            AddPackageModel package = obj.Getpackage(id);
            return View(package);
        }


        public ActionResult PakageCityDetails(int id)
        {

            PackageManager obj = new PackageManager();
            AddPackageModel package = obj.Getpackage(id);
            var cities = obj.selectCity();
            var places = obj.selectPlace();
            var packReview = db.PackageReviews.Where(x => x.Package_ID == id).ToList();
            var users = db.Users.ToList();

            List<CityMapModel> cityModels = new List<CityMapModel>();
            List<PlaceModel> placeModels = new List<PlaceModel>();
            List<PackageComment> comments = new List<PackageComment>();
            List<object> citieslatlng = new List<object>();

            string ids = package.Cities;
            string tripDays = package.TripDays;
            string placeIds = package.Places;

            if (!string.IsNullOrEmpty(ids))
            {
                string[] idsArray = ids.Split(',');
                string[] daysArray = tripDays.Split(',');
                string[] placeArray = placeIds.Split(',');

                foreach (var place in placeArray)
                {
                    var placerecords = places.Where(x => x.id == Convert.ToInt32(place)).ToList();
                    foreach (var pl in placerecords)
                    {
                        PlaceModel placeModel = new PlaceModel();
                        placeModel.place = pl.place;
                        placeModel.city_id = pl.city_id;
                        placeModel.pic = pl.pic;
                        placeModel.city = pl.city;
                        placeModels.Add(placeModel);
                    }
                }

                var inc = 0;
                foreach (var packageId in idsArray)
                {
                    var records = cities.Where(x => x.City_ID == Convert.ToInt32(packageId)).ToList();
                    foreach (var ct in records)
                    {
                        CityMapModel city = new CityMapModel();
                        city.City_Name = ct.City_Name;
                        city.City_Picture1 = ct.City_Picture1;
                        city.City_Picture2 = ct.City_Picture2;
                        city.City_Picture3 = ct.City_Picture3;
                        city.City_Picture4 = ct.City_Picture4;
                        city.City_Details = ct.City_Details;
                        city.Latitude = ct.Latitude;
                        city.Longitude = ct.Longitude;
                        city.StayDays = daysArray[inc];
                        cityModels.Add(city);
                        inc += 1;

                        Object[] citites = new Object[] { ct.City_Name, Convert.ToDouble(ct.Latitude),Convert.ToDouble(ct.Longitude), inc };

                        citieslatlng.Add(citites);
                    }
                }

                foreach (var p in packReview)
                {
                    PackageComment packageComment = new PackageComment();
                    packageComment.Name = users.FirstOrDefault(x => x.Email == p.Email).Name;
                    packageComment.Rating = p.Rating.Value;
                    packageComment.Review = p.Review;
                    comments.Add(packageComment);
                }

            }

            return Json(new { Cities = cityModels, Places = placeModels, latlngs = citieslatlng, Reviews = comments }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PakageReviews()
        {
            var packReview = db.PackageReviews.ToList();
            var users = db.Users.ToList();

            List<PackageComment> comments = new List<PackageComment>();

            foreach (var p in packReview)
            {
                PackageComment packageComment = new PackageComment();
                packageComment.Name = users.FirstOrDefault(x => x.Email == p.Email).Name;
                packageComment.Rating = p.Rating.Value;
                packageComment.Review = p.Review;
                packageComment.Gender = users.FirstOrDefault(x => x.Email == p.Email).Gender;
                comments.Add(packageComment);
            }

            return Json(new { Reviews = comments }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpdateUser(int U_Id)
        {
            CreateAdminManager obj = new CreateAdminManager();
            UserModel user = obj.GetUser(U_Id);
            if (user == null)
            {
                TempData["Message"] = "Data not Found";
                return RedirectToAction("Home");
            }

            else
            {
                if (string.IsNullOrEmpty(user.Picture))
                {
                    user.Picture = "https://imgs.search.brave.com/zXXzrLJOha0Enw8eUJBwbjBIm1DotMC8phHW2S_5jAc/rs:fit:474:225:1/g:ce/aHR0cHM6Ly90c2Ux/Lm1tLmJpbmcubmV0/L3RoP2lkPU9JUC5z/d2FZTVdBUzhjZmJ5/TUtMRWlRSjJRRDZE/NiZwaWQ9QXBp";
                }
                return View(user);
            }
        }

        [HttpPost]
        public ActionResult UpdateUser(UserModel user, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string Filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                    string Extension = Path.GetExtension(ImageFile.FileName);
                    Filename = Filename + DateTime.Now.ToString("yymmssfff") + Extension;
                    user.Picture = "~/ProjectData/" + Filename;
                    Filename = Path.Combine(Server.MapPath("~/ProjectData/"), Filename);
                    ImageFile.SaveAs(Filename);
                }

                CreateAdminManager obj = new CreateAdminManager();

                bool check = obj.UpdateUser(user);
                if (check)
                {
                    TempData["Message"] = "Data Update Successully";
                    return RedirectToAction("Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                TempData["Message"] = "Data Not Updated";
                return View();
            }
        }

        [Authorize]
        public ActionResult Booking()
        {
            if (Session["U_Id"] != null)
            {

                return View();

            }
            else
            {
                return RedirectToAction("UserLogin", "Login");
            }
            //return View();
        }
        
        [Authorize]
        public ActionResult CustomizePackage(CustomPackageModel custom)
        {
            if (Session["U_Id"] != null)
            {
                CreateAdminManager obj = new CreateAdminManager();
                UserModel user = obj.GetUser(custom.U_Id);
                custom.Name = user.Name;
                custom.Contact_No = user.Contact_Number;
                custom.CNIC = user.CNIC;
                custom.DOB = user.DOB;
                custom.Email = user.Email;
                custom.Address = user.Address;
                custom.State = user.State;
                custom.U_City = user.City;
               
                return View(custom);

            }
            else
            {
                return RedirectToAction("UserLogin", "Login");
            }
            //return View();
        }

        [HttpGet]
        public ActionResult GetCities()
        {
            PackageManager obj = new PackageManager();
            var city = obj.selectCity();
            return Json(new { Cities = city }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult GalleryPlaces()
        {
            PackageManager obj = new PackageManager();
            var places = obj.selectPlace();
            return Json(new { Places = places }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CitiesCoordinates(string data, string PickUpPoint) {

            List<object> citieslatlng = new List<object>();

            int inc = 1;

            var cityids = data.Split(',');

            var cityList = db.Citys.ToList();

            var pickUpId = cityList.FirstOrDefault(x => x.City_Name.ToLower().Equals(PickUpPoint.ToLower()));
            //Object[] pickUp = new Object[] { pickUpId.City_Name, Convert.ToDouble(pickUpId.Latitude), Convert.ToDouble(pickUpId.Longitude), inc };

            string[] array = { Convert.ToString(pickUpId.City_ID) };
            string[] newarray = array.Concat(cityids).ToArray();
            //citieslatlng.Add(pickUp);

            List<Distance> distances = new List<Distance>();

            foreach (var id in newarray) {
                var city = cityList.FirstOrDefault(x => x.City_ID.Equals(Convert.ToInt32(id)));

                distances.Add(new Distance() { 
                    Latitude = Convert.ToDouble(city.Latitude),
                    Longitude = Convert.ToDouble(city.Longitude),
                });
                
                Object[] citites = new Object[] { city.City_Name, Convert.ToDouble(city.Latitude), Convert.ToDouble(city.Longitude), inc };
                inc += 1;
                citieslatlng.Add(citites);
            }

            double totalKm = 0.0;
            for (var iter = 0; iter < distances.Count; iter++)
                if (iter < (distances.Count - 1))
                    totalKm += getDistance(distances[iter].Latitude, distances[iter].Longitude, distances[iter + 1].Latitude, distances[iter + 1].Longitude);
            var addper = totalKm * 0.30;
            int totalKmApprox = (int)Math.Ceiling(totalKm + addper);
            
            return Json(new { totalKmApprox = totalKmApprox, latlngs = citieslatlng },JsonRequestBehavior.AllowGet);
        }

        private double getDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371;
            var dLat = deg2rad(lat2 - lat1);
            var dLon = deg2rad(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d;
        }

        private double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

        [HttpGet]
        public ActionResult GetPrice(string hotels, string guider, string vehicle, string tickets)
        {
            int price = 0;
            string[] hotelids = hotels.Split(',');
            var hotelList = db.Hotels.ToList();
            foreach (var id in hotelids)
            {
                var hotel = hotelList.Where(x => x.Id == Convert.ToInt32(id)).ToList();
                //price = hotel.pr
                foreach (var pr in hotel)
                {
                    price += pr.Price.Value;
                }
            }
            var num = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(tickets) / 4));
            int gid = Convert.ToInt32(guider);
            int vid = Convert.ToInt32(vehicle);
            var GuiderCharge = db.TourGuiders.FirstOrDefault(x => x.ID == gid).Charge.Value;
            var vehicleRent = db.Vehicles.FirstOrDefault(x => x.Id == vid).Rent.Value;

            price *= num; 
            price += GuiderCharge + vehicleRent;

            //var guider = obj.selectGuider(city);
            return Json(new { Price = price}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTourGuider(string city)
        {
            PackageManager obj = new PackageManager();
            var guider = obj.selectGuider(city);
            return Json(new { Guiders = guider }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult GetVehicle(string city)
        {
            PackageManager obj = new PackageManager();
            var vehicle = obj.selectVehicle(city);
            return Json(new { Vehicles = vehicle }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPlaces(string data)
        {
            string[] ids = data.Split(',');
            PackageManager obj = new PackageManager();
            var places = obj.selectPlace();
            List<ChoicesDropdownForPlace> list = new List<ChoicesDropdownForPlace>();
            foreach (var id in ids)
            {
                //list.Add(new ChoicesDropdownForPlace { id = 1, value="0",selected=true, label="Select Place",disabled=true });
                var iter = 2;
                var placeslist = places.Where(x => x.city_id == Convert.ToInt32(id)).ToList();
                foreach (var plc in placeslist)
                {
                    ChoicesDropdownForPlace place = new ChoicesDropdownForPlace();
                    place.id = iter;
                    place.label = plc.place;
                    place.value = plc.id.ToString();
                    list.Add(place);
                    iter++;
                }

            }
            return Json(new { Places = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetHotels(string data)
        {
            string[] ids = data.Split(',');
            PackageManager obj = new PackageManager();
            var hotels = obj.selectHotel();
            List<HotelMapModel> list = new List<HotelMapModel>();
            foreach (var id in ids)
            {
                var hotelslist = hotels.Where(x => x.City_Id == Convert.ToInt32(id)).ToList();
                foreach (var htl in hotelslist)
                {
                    HotelMapModel hotel = new HotelMapModel();
                    hotel.Id = htl.Id;
                    hotel.Name = htl.Name;
                    hotel.Price = htl.Price;
                    hotel.City = htl.City;
                    hotel.Picture = htl.Picture;
                    hotel.Available_Rooms = htl.Available_Rooms;
                    hotel.Rating = htl.Rating;
                    list.Add(hotel);
                }

            }
            return Json(new { Hotels = list }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult BookingDetails(int userID, int packID)
        {
            CreateAdminManager obj = new CreateAdminManager();
            PackageManager package = new PackageManager();

            UserModel user = obj.GetUser(userID);
            AddPackageModel pack = package.Getpackage(packID);
            var hotels = package.selectHotel();

            List<HotelModel> hotelModels = new List<HotelModel>();
            string ids = pack.Hotels;

            if (!string.IsNullOrEmpty(ids))
            {
                string[] idsArray = ids.Split(',');
                foreach (var hotelId in idsArray)
                {
                    var records = hotels.Where(x => x.Id == Convert.ToInt32(hotelId)).ToList();
                    foreach (var ht in records)
                    {
                        HotelModel hotel = new HotelModel();
                        hotel.Id = ht.Id;
                        hotel.Name = ht.Name;
                        hotelModels.Add(hotel);
                    }
                }

            }
            return Json(new { User = user, Package = pack, Hotels = hotelModels }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Booking(BookingModel bookingModel)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var tranaction = db.Database.BeginTransaction();
                try
                {
                    var tax = Convert.ToInt32((bookingModel.Amount * bookingModel.No_of_Tickets) * 0.02);
                    bookingModel.Amount = (bookingModel.Amount * bookingModel.No_of_Tickets) + tax;

                    var transactionModel = new PaymentGateway().MakePayment(
                       bookingModel.CardHolderName,
                        bookingModel.CardNumber,
                        bookingModel.Month,
                        bookingModel.Year,
                        bookingModel.CVC,
                        bookingModel.Amount,
                        bookingModel.Email,
                        bookingModel.Id);

                    if (transactionModel != null)
                    {

                        var package = db.Packages.FirstOrDefault(x => x.Package_ID == bookingModel.PackageId);
                        package.No_of_Bookings = package.No_of_Bookings + bookingModel.No_of_Tickets;

                        var qrCodeText = "Name: " + bookingModel.Name + "\nCNIC: " + bookingModel.CNIC + "\nPackage: " + package.Name + "\nDate: " + package.Date.Value.ToShortDateString() + "\nNo of Tickets: " + bookingModel.No_of_Tickets + "\nStatus: Verified";
                        var qrPath = GenerateQRCode(qrCodeText);

                        Transaction transaction = new Transaction
                        {
                            Id = transactionModel.Id,
                            Amount = transactionModel.Amount,
                            Card_Number = transactionModel.Card_Number,
                            Description = transactionModel.Description,
                            Currency = transactionModel.Currency,
                            Status = transactionModel.Status,
                            Customer_Id = transactionModel.Customer_Id,
                        };

                        Booking booking = new Booking
                        {
                            U_Id = bookingModel.Id,
                            Package_ID = bookingModel.PackageId,
                            Hotel_Id = bookingModel.HotelId,
                            Name = bookingModel.Name,
                            Contact_Number = bookingModel.Contact_Number,
                            CNIC = bookingModel.CNIC,
                            DOB = bookingModel.DOB,
                            Address = bookingModel.Address,
                            State = bookingModel.State,
                            City = bookingModel.City,
                            Transaction_Id = transactionModel.Id,
                            QRCode = qrPath,
                            isReviewed = false,
                        };

                        db.Transactions.Add(transaction);
                        db.SaveChanges();
                        db.Bookings.Add(booking);
                        db.SaveChanges();
                        db.Entry(package).State = EntityState.Modified;
                        db.SaveChanges();
                        tranaction.Commit();
                        var body = "Thanks " + bookingModel.Name + "!\n\nYour booking for" + package.Name + " is Confirmed.\nWe are expecting you on " + Convert.ToDateTime(package.Date).ToShortDateString() + "\n\nPick Up Location: " + package.Pick_Up_Location + "\nYou can print your ticket(s) from booking history at our site.\n\nSincerely,\nMore To It!";
                        EmailHelper.SendEmail(bookingModel.Email,"Ticket Confirmed",body);
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }

                    return Json(false, JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    tranaction.Rollback();
                    return Json(false, JsonRequestBehavior.AllowGet);

                }
            }
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult CustomizePackageBooking(CustomPackageModel bookingModel)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var tranaction = db.Database.BeginTransaction();
                try
                {
                    //var tax = Convert.ToInt32((bookingModel.Amount * bookingModel.No_of_Tickets) * 0.02);
                    //bookingModel.Amount = (bookingModel.Amount * bookingModel.No_of_Tickets) + tax;

                    var transactionModel = new PaymentGateway().MakePayment(
                       bookingModel.HolderName,
                        bookingModel.CardNumber,
                        bookingModel.Month,
                        bookingModel.Year,
                        bookingModel.CVV,
                        bookingModel.Amount,
                        bookingModel.Email,
                        bookingModel.U_Id);

                    if (transactionModel != null)
                    {
                        Package package = new Package
                        {
                            Name = bookingModel.packName,
                            Picture = "https://5.imimg.com/data5/ZR/LV/WZ/SELLER-1703246/customized-holiday-package-500x500.jpeg",
                            Pick_Up_Location = bookingModel.pickUp,
                            No_of_Days = bookingModel.No_of_Days,
                            Price = bookingModel.Price,
                            Date = Convert.ToDateTime(bookingModel.StartDate),
                            Cities = bookingModel.Cities,
                            Places = bookingModel.Places,
                            Total_Seats = bookingModel.No_of_Tickets,
                            TripDays = bookingModel.TripDays,
                            Hotels = bookingModel.Hotels,
                            No_of_Bookings = bookingModel.No_of_Tickets,
                            GuiderID = bookingModel.Guider,
                            VehicleID = bookingModel.Vehicle,
                            IsCustomize = true,
                        };


                        Transaction transaction = new Transaction
                        {
                            Id = transactionModel.Id,
                            Amount = transactionModel.Amount,
                            Card_Number = transactionModel.Card_Number,
                            Description = transactionModel.Description,
                            Currency = transactionModel.Currency,
                            Status = transactionModel.Status,
                            Customer_Id = transactionModel.Customer_Id,
                        };
                        
                        db.Packages.Add(package);
                        db.SaveChanges();

                        var qrCodeText = "Name: " + bookingModel.Name+ "\nCNIC: " + bookingModel.CNIC + "\nPackage: " + package.Name + "\nDate: " + package.Date.Value.ToShortDateString() + "\nNo of Tickets: "+bookingModel.No_of_Tickets+"\nStatus: Verified";
                        var qrPath = GenerateQRCode(qrCodeText);

                        Booking booking = new Booking
                        {
                            U_Id = bookingModel.U_Id,
                            Package_ID = package.Package_ID,
                            Hotel_Id = bookingModel.Hotels,
                            Name = bookingModel.Name,
                            Contact_Number = bookingModel.Contact_No,
                            CNIC = bookingModel.CNIC,
                            DOB = bookingModel.DOB,
                            Address = bookingModel.Address,
                            State = bookingModel.State,
                            City = bookingModel.U_City,
                            Transaction_Id = transactionModel.Id,
                            QRCode = qrPath,
                            isReviewed = false,
                        };

                        db.Transactions.Add(transaction);
                        db.SaveChanges();
                        db.Bookings.Add(booking);
                        db.SaveChanges();
                        tranaction.Commit();
                        var body = "Thanks " + bookingModel.Name + "!\n\nYour booking for " + package.Name + " is Confirmed.\nWe are expecting you on " + Convert.ToDateTime(package.Date).ToShortDateString() + "\n\nPick Up Location: " + package.Pick_Up_Location + "\nYou can print your ticket(s) from booking history at our site.\n\nSincerely,\nMore To It!";
                        EmailHelper.SendEmail(bookingModel.Email,"Ticket Confirmed",body);
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }

                    return Json(false, JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    tranaction.Rollback();
                    return Json(false, JsonRequestBehavior.AllowGet);

                }
            }
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult Review(ReviewModel reviewModel)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var tranaction = db.Database.BeginTransaction();
                try
                {
                    if (reviewModel != null)
                    {
                        var user = db.Users.FirstOrDefault(x => x.U_Id == reviewModel.U_Id);
                        var package = db.Packages.FirstOrDefault(x => x.Package_ID == reviewModel.packid);
                       
                        var booking = db.Bookings.FirstOrDefault(x => x.BookingId == reviewModel.bookingid);
                        booking.isReviewed = true;


                        PackageReview packageReview = new PackageReview
                        {
                            Rating = reviewModel.packRating,
                            Review = reviewModel.packReview,
                            Package_ID = reviewModel.packid,
                            Email = user.Email,
                        };
                        
                        GuiderReview guiderReview = new GuiderReview
                        {
                            Rating = reviewModel.guiderRating,
                            Review = reviewModel.guiderReview,
                            Guider_ID = package.GuiderID,
                            Email = user.Email,
                        };
                        
                        VehicleReview vehicleReview = new VehicleReview
                        {
                            Rating = reviewModel.vehicleRating,
                            Review = reviewModel.vehicleReview,
                            Vehicle_ID = package.VehicleID,
                            Email = user.Email,
                        };

                       

                        db.PackageReviews.Add(packageReview);
                        db.SaveChanges();
                        db.GuiderReviews.Add(guiderReview);
                        db.SaveChanges();
                        db.VehicleReviews.Add(vehicleReview);
                        db.SaveChanges();
                        db.Entry(booking).State = EntityState.Modified;
                        db.SaveChanges();
                        tranaction.Commit();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }

                    return Json(false, JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    tranaction.Rollback();
                    return Json(false, JsonRequestBehavior.AllowGet);

                }
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult BookingHistory(int U_Id)
        {
            PackageManager obj = new PackageManager();
            List<BookingMapModel> booking = obj.GetBookingHistory(U_Id);
            return View(booking);
        }

        [Authorize]
        [HttpGet]
        public ActionResult SummaryDetails(int bookingId)
        {
            PackageManager obj = new PackageManager();
            SummaryModel summary = obj.Summary(bookingId);
            return Json(new { Summary = summary }, JsonRequestBehavior.AllowGet);
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult PrintTicket(int bookingId)
        {
            return View(db.Bookings.FirstOrDefault(x => x.BookingId == bookingId));
        }

        [HttpPost]
        public ActionResult GuiderApplication(TourGuiderModel guider, HttpPostedFileBase file, HttpPostedFileBase ProfileImage)
        {
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    TempData["Message"] = "Upload Resume!";
                    return RedirectToAction("Home");
                }else if (ProfileImage == null)
                {
                    TempData["Message"] = "Upload Profile Image!";
                    return RedirectToAction("Home");
                }
                else
                {
                    var isExists = db.TourGuiders.Any(x => x.Licence_No == guider.Licence_No);

                    if(isExists) {
                        TempData["Message"] = "Licence Number Already Exists!";
                        return RedirectToAction("Home");
                    }
                    else { 
                        if (Path.GetExtension(file.FileName) == ".pdf" || Path.GetExtension(file.FileName) == ".docx" || Path.GetExtension(file.FileName) == ".doc")
                        {
                            string Filename = Path.GetFileNameWithoutExtension(file.FileName);
                            string Extension = Path.GetExtension(file.FileName);
                            Filename = Filename + DateTime.Now.ToString("yymmssfff") + Extension;
                            guider.FilePath = "~/ProjectData/GuiderResume/" + Filename;
                            Filename = Path.Combine(Server.MapPath("~/ProjectData/GuiderResume/"), Filename);
                            file.SaveAs(Filename);

                            if (Path.GetExtension(ProfileImage.FileName) == ".jpg" || Path.GetExtension(ProfileImage.FileName) == ".jpeg" || Path.GetExtension(ProfileImage.FileName) == ".png")
                            {
                                string Filename1 = Path.GetFileNameWithoutExtension(ProfileImage.FileName);
                                string Extension1 = Path.GetExtension(ProfileImage.FileName);
                                Filename1 = Filename1 + DateTime.Now.ToString("yymmssfff") + Extension1;
                                guider.Img = "~/ProjectData/GuiderProfileImg/" + Filename1;
                                Filename1 = Path.Combine(Server.MapPath("~/ProjectData/GuiderProfileImg/"), Filename1);
                                ProfileImage.SaveAs(Filename1);

                            }

                            PackageManager obj = new PackageManager();
                            int g_id = obj.GuiderJobApplication(guider);
                            if (g_id > 0)
                            {
                                TempData["Message"] = "Your Tour Guide Application is Submitted, Application Id: " + g_id;
                                var body = "" + guider.Name + "!\n\nThanks for taking the time to apply for our position. We appreciate your interest in More To It!, We're currently in the process of taking applications for this position. If you are selected to continue into our interview process, our human resource department will be contact with you in the next couple of days.\n\nThank You,\n\nMore To It!";
                                EmailHelper.SendEmail(guider.Email,"Thank you for your application",body);
                                return RedirectToAction("Home");
                            }

                            else
                            {
                                TempData["Message"] = "Your Tour Guide Application is not Submitted Successfully!";
                                return RedirectToAction("Home");
                            }
                        }
                        else
                        {
                            TempData["Message"] = "PLease Share Proper Files!";
                            return RedirectToAction("Home");
                        }
                    }
                }
            }
            else
            {
                TempData["Message"] = "Application Unsubmitted, Kindly Fill The Form Completely!!";
                return RedirectToAction("Home");
            }
        }

        [HttpPost]
        public ActionResult VehicleApplication(VehicleModel vehicle, IEnumerable<HttpPostedFileBase> ImageFile, HttpPostedFileBase PdfFile)
        {
            if (ModelState.IsValid)
            {
                PackageManager obj = new PackageManager();
                if (ImageFile == null)
                {
                    TempData["Message"] = "Upload Vehicle Images!";
                    return RedirectToAction("Home");
                }
                else if (PdfFile == null)
                {
                    TempData["Message"] = "Upload Vehicle Inspection Pdf!";
                    return RedirectToAction("Home");
                }
                else
                {
                    var isExists = db.Vehicles.Any(x => x.Vehicle_No == vehicle.Vehicle_No);
                    if (isExists)
                    {
                        TempData["Message"] = "Vehicle Number Already Exists!";
                        return RedirectToAction("Home");
                    }
                    else { 
                        string[] patharray;
                        string upload = string.Empty;
                        foreach (var item in ImageFile)
                        {
                            if (Path.GetExtension(item.FileName) == ".jpg" || Path.GetExtension(item.FileName) == ".jpeg" || Path.GetExtension(item.FileName) == ".png")
                            {
                                string Filename = Path.GetFileNameWithoutExtension(item.FileName);
                                string Extension = Path.GetExtension(item.FileName);
                                Filename = Filename + DateTime.Now.ToString("yymmssfff") + Extension;

                                var Filename1 = "~/ProjectData/Vehicles/" + Filename;
                                upload += Filename1 + ",";
                                Filename = Path.Combine(Server.MapPath("~/ProjectData/Vehicles/"), Filename);
                                item.SaveAs(Filename);
                            }
                            else
                            {
                                TempData["Message"] = "This is not Image file!";
                            }
                        }

                        if (Path.GetExtension(PdfFile.FileName) == ".pdf" || Path.GetExtension(PdfFile.FileName) == ".docx" || Path.GetExtension(PdfFile.FileName) == ".doc")
                        {
                            string PdfFilename = Path.GetFileNameWithoutExtension(PdfFile.FileName);
                            string PdfExtension = Path.GetExtension(PdfFile.FileName);
                            PdfFilename = PdfFilename + DateTime.Now.ToString("yymmssfff") + PdfExtension;
                            var Filename2 = "~/ProjectData/VehicleInspection/" + PdfFilename;
                            upload += Filename2 + ",";
                            PdfFilename = Path.Combine(Server.MapPath("~/ProjectData/VehicleInspection/"), PdfFilename);
                            PdfFile.SaveAs(PdfFilename);
                        }

                        patharray = upload.Split(',');
                        var pic = string.Empty;
                        foreach (var p in patharray)
                        {
                            pic = p + ", " + pic;
                        }

                        pic = pic.Substring(1, pic.Length - 3);

                        vehicle.Vehicle_Pictures = pic;

                        int v_id = obj.VehicleRentApplication(vehicle);
                        if (v_id > 0)
                        {
                            TempData["Message"] = "Application for Vehicle Sent Successfully!";
                            var body = "" + vehicle.User_Name + "!\n\nThanks for taking the time to apply your vehicle in our service. We appreciate your interest in More To It!, We're currently in the process of taking applications for our service. If your vehicle is in good condition to proceed, our human resource department will be contact with you in the next couple of days.\n\nThank You,\nMore To It!";
                            EmailHelper.SendEmail(vehicle.Email,"Thank you for your application", body);
                            return RedirectToAction("Home");
                        }

                        else
                        {
                            TempData["Message"] = "Application is not sent!";
                        }
                    }
                }
            }
            else
            {
                TempData["Message"] = "Application is not send Kindly Fill Complete Form!";
            }
            return RedirectToAction("Home");
        }

        private string GenerateQRCode(string qrcodeText)
        {
            string folderPath = "/ProjectData/TicketQrCode";
            string imagePath = $"/ProjectData/TicketQrCode/{Guid.NewGuid()}-QrCode.jpg";
            // If the directory doesn't exist then create it.
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(qrcodeText);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return imagePath;
        }

    }

    public class Distance
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class PackageComment
    {
        public int Rating { get; set; }
        public string Review { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }
}