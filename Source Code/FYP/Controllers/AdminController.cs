using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP.Models;
using FYP.Manager;
using FYP.Filters;
using System.IO;
using System.Data.Entity;

namespace FYP.Controllers
{
    [AuthorizedUser]
    [IsAdmin]
    public class AdminController : Controller
    {
        MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities();

        // GET: Admin
        public ActionResult Index(DashboardModel dashboardModel)
        {
            dashboardModel.total_users = db.Users.Count();
            dashboardModel.total_admins = db.Admins.Count();
            dashboardModel.total_cities = db.Citys.Count();
            dashboardModel.total_places = db.FamousPlaces.Count();
            dashboardModel.total_hotels = db.Hotels.Count();
            dashboardModel.total_packages = db.Packages.Count();
            dashboardModel.total_bookings = db.Bookings.Count();
            dashboardModel.total_tourGuiderEmployee = db.TourGuiders.Count(x => x.Status == true);
            dashboardModel.total_vehicle = db.Vehicles.Count(x => x.Status == true);
            return View(dashboardModel);
        }

        public ActionResult TourGuiderBadge()
        {
            int guiderApplications = db.TourGuiders.Count(x => x.Status == false); 
            return PartialView("_GuiderApplications", guiderApplications);
        }
        
        public ActionResult VehicleBadge()
        {
            int vehicleApplications = db.Vehicles.Count(x => x.Status == false); 
            return PartialView("_VehicleApplications", vehicleApplications);
        }
        
        public ActionResult Badge()
        {
            int guiderApplications = db.TourGuiders.Count(x => x.Status == false); 
            int vehicleApplications = db.Vehicles.Count(x => x.Status == false);
            var total = guiderApplications + vehicleApplications;
            return PartialView("_Badge", total);
        }

        [HttpGet]
        public ActionResult CreateAdmin()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult CreateAdmin(CreateAdminModel Admins)
        {
            if (ModelState.IsValid)
            {
                CreateAdminManager obj = new CreateAdminManager();
                Admins.UserRole = 1;
                Admins.IsActive = true;
                int a_id = obj.AddAdmin(Admins);
                if (a_id > 0)
                {
                    TempData["Message"] = "Admin Created Successfuly and Admin Id is " + a_id;
                    return RedirectToAction("ViewAllAdmin");
                }
                
                else
                {
                    TempData["Message"] = "Admin Not Created!";
                }
            }
            else
            {
                TempData["Message"] = "User Not Created Kindly Fill Complete Form!";
            }
            return View(Admins);
        }

        [HttpGet]
        public ActionResult UpdateAdmin(int User_ID)
        {
            CreateAdminManager obj = new CreateAdminManager();
            CreateAdminModel user = obj.GetAdmin(User_ID);
            if (user == null)
            {
                TempData["Message"] = "Data not Found";
                return RedirectToAction("ViewAllAdmin");
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
        public ActionResult UpdateAdmin(CreateAdminModel user)
        {
            if (ModelState.IsValid)
            {
                CreateAdminManager obj = new CreateAdminManager();
                user.UserRole = 1;
                bool check = obj.UpdateAdmin(user);
                if (check)
                {
                    TempData["Message"] = "Data Update Successully";
                    return RedirectToAction("ViewAllAdmin");
                }
                else
                {
                    return View(user);
                }
            }
            else
            {
                TempData["Message"] = "Data Not Updated";
                return View(user);
            }
        }

        public ActionResult ViewAllAdmin()
        {
            CreateAdminManager obj = new CreateAdminManager();
            List<CreateAdminModel> Admin = obj.selectAdmin();
            return View(Admin);
        }

        [HttpGet]
        public ActionResult AddPackage()
        {
            MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities();
            ViewBag.PickLocation = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
            //ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
            //ViewBag.Place = new SelectList(db.FamousPlaces.OrderBy(x => x.Place_Name), "Place_Id", "Place_Name");
            //ViewBag.Hotel = new SelectList(db.Hotels.OrderBy(x => x.Name), "Hotel_Id", "Name");
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult AddPackage(AddPackageModel packageModel, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                PackageManager obj = new PackageManager();
                if (ImageFile == null)
                {
                    ViewBag.PickLocation = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
                    //ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                    //ViewBag.Place = new SelectList(db.FamousPlaces.OrderBy(x => x.Place_Name), "Place_Id", "Place_Name");
                    //ViewBag.Hotel = new SelectList(db.Hotels.OrderBy(x => x.Name), "Hotel_Id", "Name");
                    TempData["Message"] = "Upload Image for Package!";
                    return View(packageModel);
                }
                else
                {
                    if (Path.GetExtension(ImageFile.FileName) == ".jpg" || Path.GetExtension(ImageFile.FileName) == ".jpeg" || Path.GetExtension(ImageFile.FileName) == ".png")
                    {
                        string Filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                        string Extension = Path.GetExtension(ImageFile.FileName);
                        Filename = Filename + DateTime.Now.ToString("yymmssfff") + Extension;
                        packageModel.Picture = "~/ProjectData/Packages/" + Filename;
                        string path = Path.Combine(Server.MapPath("~/ProjectData/Packages/"), Filename);
                        ImageFile.SaveAs(path);


                        int a_id = obj.CreatePackage(packageModel);
                        if (a_id > 0)
                        {
                            TempData["Message"] = "Package Created Successfuly";
                            return RedirectToAction("ViewAllPackage");
                        }

                        else
                        {
                            ViewBag.PickLocation = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
                            //ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                            //ViewBag.Place = new SelectList(db.FamousPlaces.OrderBy(x => x.Place_Name), "Place_Id", "Place_Name");
                            //ViewBag.Hotel = new SelectList(db.Hotels.OrderBy(x => x.Name), "Hotel_Id", "Name");
                            TempData["Message"] = "Package Not Created!";
                        }
                    }
                    else
                    {
                        ViewBag.PickLocation = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
                        //ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                        //ViewBag.Place = new SelectList(db.FamousPlaces.OrderBy(x => x.Place_Name), "Place_Id", "Place_Name");
                        //ViewBag.Hotel = new SelectList(db.Hotels.OrderBy(x => x.Name), "Hotel_Id", "Name");
                        TempData["Message"] = "This is not Image file!";
                    }

                }
            }
            else
            {
                ViewBag.PickLocation = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
                //ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                //ViewBag.Place = new SelectList(db.FamousPlaces.OrderBy(x => x.Place_Name), "Place_Id", "Place_Name");
                //ViewBag.Hotel = new SelectList(db.Hotels.OrderBy(x => x.Name), "Hotel_Id", "Name");
                TempData["Message"] = "Package Not Created Kindly Fill Complete Form!";
            }
            return View(packageModel);
        }

        public ActionResult ViewAllPackage()
        {
            PackageManager obj = new PackageManager();
            List<AddPackageModel> package = obj.selectPackage();
            return View(package);
        }

        public ActionResult ViewAllCity()
        {
            PackageManager obj = new PackageManager();
            List<AddCityModel> city = obj.selectCity();
            return View(city);
        }

        public ActionResult ViewAllPlace()
        {
            PackageManager obj = new PackageManager();
            List<AddFamousPlaceModel> place = obj.AllPlaces();
            return View(place);
        }

        public ActionResult ViewAllHotels()
        {
            PackageManager obj = new PackageManager();
            List<HotelMapModel> hotel = obj.selectHotel();
            return View(hotel);
        }

        [HttpGet]
        public ActionResult PackageDetails(int id)
        {
            PackageManager obj = new PackageManager();
            AddPackageModel package = obj.Getpackage(id);
            return Json(new { packageDetail = package }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CityDetails(int id)
        {
            PackageManager obj = new PackageManager();
            AddCityModel City = obj.GetCity(id);
            return Json(new { cityDetail = City }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PlaceDetails(int id)
        {
            PackageManager obj = new PackageManager();
            AddFamousPlaceModel Place = obj.GetPlace(id);
            return Json(new { placeDetail = Place }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult HotelDetails(int id)
        {
            PackageManager obj = new PackageManager();
            HotelModel hotel = obj.GetHotel(id);
            return Json(new { hotelDetail = hotel }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetGuiderVehicle(string city)
        {
            PackageManager obj = new PackageManager();
            var guider = obj.selectGuider(city);
            var vehicle = obj.selectVehicle(city);

            return Json(new { Guiders = guider, Vehicles = vehicle }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult GuiderVehicle(string city)
        {
            int cityId = Convert.ToInt32(city);
            string pickup = db.Citys.FirstOrDefault(x => x.City_ID == cityId).City_Name;
            PackageManager obj = new PackageManager();
            var guider = obj.selectGuider(pickup);
            var vehicle = obj.selectVehicle(pickup);

            return Json(new { Guiders = guider, Vehicles = vehicle }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCities()
        {
            PackageManager obj = new PackageManager();
            var city = obj.selectCity();
            return Json(new { Cities = city }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPlaces(string data)
        {
            string[] ids = data.Split(',');
            PackageManager obj = new PackageManager();
            var places = obj.selectPlace();
            List<PlaceModel> list = new List<PlaceModel>();
            foreach (var id in ids)
            {
                var placeslist = places.Where(x => x.city_id == Convert.ToInt32(id)).ToList();
                foreach (var plc in placeslist)
                {
                    PlaceModel place = new PlaceModel();
                    place.id = plc.id;
                    place.place = plc.place;
                    place.city = plc.city;
                    list.Add(place);
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
                    list.Add(hotel);
                }

            }
            return Json(new { Hotels = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddCity()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult AddCity(AddCityModel cityModel, IEnumerable<HttpPostedFileBase> ImageFile)
        {
            if (ModelState.IsValid)
            {
                PackageManager obj = new PackageManager();
                if (ImageFile == null)
                {
                    TempData["Message"] = "Upload Image for City!";
                    return View(cityModel);
                }
                else
                {
                    string[] patharray;
                    string upload = string.Empty;
                    foreach (var item in ImageFile)
                    {
                        if (Path.GetExtension(item.FileName) == ".jpg" || Path.GetExtension(item.FileName) == ".jpeg" || Path.GetExtension(item.FileName) == ".png")
                        {
                            string Filename = Path.GetFileNameWithoutExtension(item.FileName);
                            string Extension = Path.GetExtension(item.FileName);
                            Filename = Filename + DateTime.Now.ToString("yymmssfff") + Extension;

                            var Filename1 = "~/ProjectData/Citys/" + Filename;
                            upload += Filename1 + ",";
                            Filename = Path.Combine(Server.MapPath("~/ProjectData/Citys/"), Filename);
                            item.SaveAs(Filename);
                        }
                        else
                        {
                            TempData["Message"] = "This is not Image file!";
                        }
                    }

                    patharray = upload.Split(',');
                    cityModel.City_Picture1 = patharray[0].ToString();
                    cityModel.City_Picture2 = patharray[1].ToString();
                    cityModel.City_Picture3 = patharray[2].ToString();
                    cityModel.City_Picture4 = patharray[3].ToString();

                    int a_id = obj.AddCity(cityModel);
                    if (a_id > 0)
                    {
                        TempData["Message"] = "City Is Added Successfully!";
                        return RedirectToAction("ViewAllCity");
                    }

                    else
                    {
                        TempData["Message"] = "City Is Not Added!";
                    }
                }
            }
            else
            {
                TempData["Message"] = "City Is Not Added Kindly Fill Complete Form!";
            }
            return View(cityModel);
        }

        [HttpGet]
        public ActionResult AddPlace()
        {
            MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities();
            ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult AddPlace(AddFamousPlaceModel placeModel, IEnumerable<HttpPostedFileBase> ImageFile)
        {
            if (ModelState.IsValid)
            {
                PackageManager obj = new PackageManager();
                if (ImageFile == null)
                {
                    TempData["Message"] = "Upload Image for Place!";
                    ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                    return View(placeModel);
                }
                else
                {
                    string[] patharray;
                    string upload = string.Empty;
                    foreach (var item in ImageFile)
                    {
                        if (Path.GetExtension(item.FileName) == ".jpg" || Path.GetExtension(item.FileName) == ".jpeg" || Path.GetExtension(item.FileName) == ".png")
                        {
                            string Filename = Path.GetFileNameWithoutExtension(item.FileName);
                            string Extension = Path.GetExtension(item.FileName);
                            Filename = Filename + DateTime.Now.ToString("yymmssfff") + Extension;

                            var Filename1 = "~/ProjectData/Places/" + Filename;
                            upload += Filename1 + ",";
                            Filename = Path.Combine(Server.MapPath("~/ProjectData/Places/"), Filename);
                            item.SaveAs(Filename);
                        }
                        else
                        {
                            ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                            TempData["Message"] = "This is not Image file!";
                        }
                    }

                    patharray = upload.Split(',');
                    placeModel.Place_Pic1 = patharray[0].ToString();
                    placeModel.Place_Pic2 = patharray[1].ToString();
                    placeModel.Place_Pic3 = patharray[2].ToString();
                    placeModel.Place_Pic4 = patharray[3].ToString();

                    int a_id = obj.AddPlace(placeModel);
                    if (a_id > 0)
                    {
                        TempData["Message"] = "Place Is Added Successfully!";
                        return RedirectToAction("ViewAllPlace");
                    }

                    else
                    {
                        ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                        TempData["Message"] = "Place Is Not Added!";
                    }
                }
            }
            else
            {
                ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                TempData["Message"] = "Place Is Not Added Kindly Fill Complete Form!";
            }
            return View(placeModel);
        }

        //[HttpGet]
        //public ActionResult AddHotel()
        //{
        //    MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities();
        //    ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
        //    ViewBag.Message = "";
        //    return View();
        //}
        
        //[HttpPost]
        //public ActionResult AddHotel(HotelModel hotelModel, IEnumerable<HttpPostedFileBase> ImageFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        PackageManager obj = new PackageManager();
        //        if (ImageFile == null)
        //        {
        //            TempData["Message"] = "Upload Image for Place!";
        //            return View();
        //        }
        //        else
        //        {
        //            string[] patharray;
        //            string upload = string.Empty;
        //            foreach (var item in ImageFile)
        //            {
        //                if (Path.GetExtension(item.FileName) == ".jpg" || Path.GetExtension(item.FileName) == ".jpeg" || Path.GetExtension(item.FileName) == ".png")
        //                {
        //                    string Filename = Path.GetFileNameWithoutExtension(item.FileName);
        //                    string Extension = Path.GetExtension(item.FileName);
        //                    Filename = Filename + DateTime.Now.ToString("yymmssfff") + Extension;

        //                    var Filename1 = "~/ProjectData/Hotels/" + Filename;
        //                    upload += Filename1 + ",";
        //                    Filename = Path.Combine(Server.MapPath("~/ProjectData/Hotels/"), Filename);
        //                    item.SaveAs(Filename);
        //                }
        //                else
        //                {
        //                    TempData["Message"] = "This is not Image file!";
        //                }
        //            }

        //            patharray = upload.Split(',');
        //            var pic = string.Empty;
        //            foreach (var p in patharray)
        //            {
        //                pic = p + ", " + pic;
        //            }

        //            pic = pic.Substring(1, pic.Length - 3);

        //            hotelModel.Picture = pic;

        //            int a_id = obj.AddHotel(hotelModel);
        //            if (a_id > 0)
        //            {
        //                TempData["Message"] = "Hotel Is Added Successfully!";
        //                return RedirectToAction("ViewAllHotels");
        //            }

        //            else
        //            {
        //                TempData["Message"] = "Hotel Is Not Added!";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        TempData["Message"] = "Hotel Is Not Added Kindly Fill Complete Form!";
        //    }
        //    return View();
        //}

        [HttpGet]
        public ActionResult UpdateCity(int City_Id)
        {
            PackageManager obj = new PackageManager();
            AddCityModel city = obj.GetCity(City_Id);
            if (city == null)
            {
                TempData["Message"] = "Data not Found";
                return RedirectToAction("ViewAllCity");
            }
            else
            {
                return View(city);
            }
        }

        [HttpPost]
        public ActionResult UpdateCity(AddCityModel city)
        {
            if (ModelState.IsValid)
            {
                PackageManager obj = new PackageManager();
                bool check = obj.UpdateCity(city);
                if (check)
                {
                    TempData["Message"] = "Data Update Successully";
                    return RedirectToAction("ViewAllCity");
                }
                else
                {
                    return View(city);
                }
            }
            else
            {
                TempData["Message"] = "Data Not Updated";
                return View(city);
            }
        }

        [HttpGet]
        public ActionResult UpdatePlace(int Place_Id)
        {
            ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");

            PackageManager obj = new PackageManager();
            AddFamousPlaceModel place = obj.GetPlace(Place_Id);
            if (place == null)
            {
                TempData["Message"] = "Data not Found";
                return RedirectToAction("ViewAllPLace");
            }
            else
            {
                ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                return View(place);
            }
        }

        [HttpPost]
        public ActionResult UpdatePlace(AddFamousPlaceModel place)
        {
            if (ModelState.IsValid)
            {
                PackageManager obj = new PackageManager();
                bool check = obj.UpdatePlace(place);
                if (check)
                {
                    TempData["Message"] = "Data Update Successully";
                    return RedirectToAction("ViewAllPlace");
                }
                else
                {
                    ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                    return View(place);
                }
            }
            else
            {
                ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                TempData["Message"] = "Data Not Updated";
                return View(place);
            }
        }

        [HttpGet]
        public ActionResult UpdatePackage(int Pack_Id)
        {
            ViewBag.PickLocation = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "Id", "City");
            ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
            ViewBag.Place = new SelectList(db.FamousPlaces.OrderBy(x => x.Place_Name), "Place_Id", "Place_Name");
            ViewBag.Hotel = new SelectList(db.Hotels.OrderBy(x => x.Name), "Hotel_Id", "Name");
            PackageManager obj = new PackageManager();
            AddPackageModel pack = obj.Getpackage(Pack_Id);
            if (pack == null)
            {
                TempData["Message"] = "Data not Found";
                return RedirectToAction("ViewAllPackage");
            }
            else
            {
                ViewBag.PickLocation = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "Id", "City");
                ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                ViewBag.Place = new SelectList(db.FamousPlaces.OrderBy(x => x.Place_Name), "Place_Id", "Place_Name");
                ViewBag.Hotel = new SelectList(db.Hotels.OrderBy(x => x.Name), "Hotel_Id", "Name");
                return View(pack);
            }
        }

        [HttpPost]
        public ActionResult UpdatePackage(AddPackageModel pack)
        {
            if (ModelState.IsValid)
            {
                PackageManager obj = new PackageManager();

                bool check = obj.UpdatePackage(pack);
                if (check)
                {
                    TempData["Message"] = "Data Update Successully";
                    return RedirectToAction("ViewAllPackage");
                }
                else
                {
                    ViewBag.PickLocation = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "Id", "City");
                    ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                    ViewBag.Place = new SelectList(db.FamousPlaces.OrderBy(x => x.Place_Name), "Place_Id", "Place_Name");
                    ViewBag.Hotel = new SelectList(db.Hotels.OrderBy(x => x.Name), "Hotel_Id", "Name");
                    return View(pack);
                }
            }
            else
            {
                ViewBag.PickLocation = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "Id", "City");
                ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");
                ViewBag.Place = new SelectList(db.FamousPlaces.OrderBy(x => x.Place_Name), "Place_Id", "Place_Name");
                ViewBag.Hotel = new SelectList(db.Hotels.OrderBy(x => x.Name), "Hotel_Id", "Name");
                TempData["Message"] = "Data Not Updated";
                return View(pack);
            }
        }
        
        //[HttpGet]
        //public ActionResult UpdateHotel(int Hotel_Id)
        //{
        //    ViewBag.City = new SelectList(db.Citys.OrderBy(x => x.City_Name), "City_Id", "City_Name");

        //    PackageManager obj = new PackageManager();
        //    HotelModel hotel = obj.GetHotel(Hotel_Id);
        //    if (hotel == null)
        //    {
        //        TempData["Message"] = "Data not Found";
        //        return RedirectToAction("ViewAllHotels");
        //    }
        //    else
        //    {
        //        return View(hotel);
        //    }
        //}

        //[HttpPost]
        //public ActionResult UpdateHotel(HotelModel hotel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        PackageManager obj = new PackageManager();

        //        bool check = obj.UpdateHotel(hotel);
        //        if (check)
        //        {
        //            TempData["Message"] = "Data Update Successully";
        //            return RedirectToAction("ViewAllHotels");
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        TempData["Message"] = "Data Not Updated";
        //        return View();
        //    }
        //}

        [HttpGet]
        public ActionResult PaymentHistory()
        {
            
            return View(db.Transactions.ToList());
        }

        [HttpGet]
        public ActionResult TourGuiderRequest()
        {
            return View(db.TourGuiders.OrderByDescending(x => x.ID).Where(x=>x.Status == false).ToList());
        }
        
        [HttpGet]
        public ActionResult TourGuider()
        {
            return View(db.TourGuiders.OrderByDescending(x => x.ID).Where(x=>x.Status == true).ToList());
        }
        
        [HttpGet]
        public ActionResult JobAccepted(int id, string charge)
        {
            var guider = db.TourGuiders.FirstOrDefault(x => x.ID == id);
            guider.Status = true;
            guider.Charge = Convert.ToInt32(charge);
            db.Entry(guider).State = EntityState.Modified;
            db.SaveChanges();
            var body = "Dear " + guider.Name + ",\n\nI am pleased to extend the following offer of employment to you on behalf of More To It!. You have been selected as the best candidate for the Tour Guider position.\n\nCongratulations!\n\nYou will receive our job offer letter soon.\n\nSincerely,\nZeeshan Waqar\nHR Manager\nMore To It!";
            EmailHelper.SendEmail(guider.Email,"Job offer from More To It!",body);
            return RedirectToAction("TourGuider");
        }
        
        [HttpGet]
        public ActionResult JobDeclined(int id)
        {
            var guider = db.TourGuiders.FirstOrDefault(x => x.ID == id);
            db.TourGuiders.Remove(guider);
            db.SaveChanges();
            var body = "Dear " + guider.Name + ",\n\nThanks for taking the time to consider More To It!. Our hiring team reviewed your application and we'd like to inform you that we are not able to advance you to the next round for the Tour Guider position at this time. We encourage you to apply again in the future, if you find an open role at our company that suits you. \n\nRegards,\nZeeshan Waqar\nHR Manager\nMore To It!";
            EmailHelper.SendEmail(guider.Email,"Application for the Tour Guider position",body);
            return RedirectToAction("TourGuiderRequest");
        }

        [HttpGet]
        public ActionResult GuiderDetails(int id)
        {
            PackageManager obj = new PackageManager();
            TourGuiderModel GuiderInfo = obj.GetGuider(id);
            return Json(new { guiderDetail = GuiderInfo }, JsonRequestBehavior.AllowGet);
        }

        public FileResult GetReport(int GuiderId)
        {
            var filepath = db.TourGuiders.FirstOrDefault(x => x.ID == GuiderId).FilePath;
            string ReportURL = Server.MapPath(filepath);
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(ReportURL);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", Path.GetFileName(filepath));
        }
        public FileResult GetInspectionReport(int UserId)
        {
            var img = db.Vehicles.FirstOrDefault(x => x.Id == UserId).Vehicle_Pictures;
            var pathArray = img.Split(',');
            var filepath = pathArray[0];
            string ReportURL = Server.MapPath(filepath);
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(ReportURL);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", Path.GetFileName(filepath));
        }

        [HttpGet]
        public ActionResult VehicleRentRequest()
        {
            return View(db.Vehicles.OrderByDescending(x =>x.Id).Where(x => x.Status == false).ToList());
        }
        
        [HttpGet]
        public ActionResult VehicleRent()
        {
            return View(db.Vehicles.OrderByDescending(x => x.Id).Where(x=>x.Status == true).ToList());
        }

        [HttpGet]
        public ActionResult VehicleDetails(int id)
        {
            PackageManager obj = new PackageManager();
            VehicleModel vehicleInfo = obj.GetVehicle(id);
            return Json(new { vehicleDetail = vehicleInfo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VehicleAccepted(int id, string rent)
        {

            var vehicle = db.Vehicles.FirstOrDefault(x => x.Id == id);
            vehicle.Status = true;
            vehicle.Rent = Convert.ToInt32(rent);
            db.Entry(vehicle).State = EntityState.Modified;
            db.SaveChanges();
            var body = "Dear " + vehicle.User_Name + ",\n\nI am pleased to extend the following offer of vehicle service to you on behalf of More To It!. Your vehicle have been selected as the best vehicle.\n\nCongratulations!\n\nYou will receive our confirmation letter soon.\n\nSincerely,\n\nZeeshan Waqar\nHR Manager\nMore To It!";
            EmailHelper.SendEmail(vehicle.Email,"Vehicle Service Confirmation offer from More To It!",body);
            return RedirectToAction("VehicleRent");
        }

        [HttpGet]
        public ActionResult VehicleDeclined(int id)
        {
            var vehicle = db.Vehicles.FirstOrDefault(x => x.Id == id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            var body = "Dear " + vehicle.User_Name + ",\n\nThanks for taking the time to consider More To It!. Our hiring team reviewed your vehicle application and we'd like to inform you that we are not able to advance your vehicle to the next round for our service at this time.\n\nWe encourage you to apply again in the future, if you find an open role at our company that suits you.\n\nRegards,\nZeeshan Waqar\nHR Manager\nMore To It!";
            EmailHelper.SendEmail(vehicle.Email, "Application for vehicle service", body);
            return RedirectToAction("VehicleRentRequest");
        }

        [HttpGet]
        public ActionResult CompanyVehicle()
        {
            MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities();
            ViewBag.City = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult CompanyVehicle(VehicleModel vehicleModel, IEnumerable<HttpPostedFileBase> ImageFile)
        {
                  
            if (ModelState.IsValid)
            {
                PackageManager obj = new PackageManager();
                if (ImageFile == null)
                {
                    TempData["Message"] = "Upload Image for Vehicle!";
                    ViewBag.City = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
                    return View(vehicleModel);
                }
                else
                {
                    var isExist = db.Vehicles.Any(x => x.Vehicle_No == vehicleModel.Vehicle_No);

                    if (isExist)
                    {
                        TempData["Message"] = "Vehicle Number Already Exists!";
                        ViewBag.City = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
                        return View(vehicleModel);
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
                                ViewBag.City = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
                                TempData["Message"] = "This is not Image file!";
                            }
                        }

                        patharray = upload.Split(',');
                        var pic = string.Empty;
                        foreach (var p in patharray)
                        {
                            pic = p + ", " + pic;
                        }

                        pic = pic.Substring(1, pic.Length - 3);

                        vehicleModel.Vehicle_Pictures = pic;

                        int a_id = obj.CompanyVehicle(vehicleModel);
                        if (a_id > 0)
                        {
                            TempData["Message"] = "vehicle Is Added Successfully!";
                            return RedirectToAction("VehicleRent");
                        }

                        else
                        {
                            ViewBag.City = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
                            TempData["Message"] = "Vehicle Is Not Added!";
                        }
                    }

                }
            }
            else
            {
                ViewBag.City = new SelectList(db.PickUpLocations.OrderBy(x => x.City), "City", "City");
                TempData["Message"] = "Vehicle Is Not Added Kindly Fill Complete Form!";
            }
            return View(vehicleModel);
        }
    }
}