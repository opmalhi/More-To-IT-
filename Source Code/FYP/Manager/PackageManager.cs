using FYP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FYP.Manager
{
    public class PackageManager
    {
        MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities();

        int guiderid = 0;

        public int CreatePackage(AddPackageModel package)
        {
            try
            {
                Package pack = new Package()
                {
                    Name = package.Name,
                    No_of_Days = package.No_of_Days,
                    TripDays = package.TripDays,
                    Pick_Up_Location = package.Pick_Up_Location,
                    Price = package.Price,
                    Date = package.Date,
                    Picture = package.Picture,
                    Cities = package.Cities,
                    Places = package.Places,
                    Hotels = package.Hotels,
                    Inclusions = package.Inclusions,
                    Exclusions = package.Exclusions,
                    No_of_Bookings = 0,
                    Total_Seats = package.Total_Seats,
                    IsCustomize = false,
                    GuiderID = package.GuiderID,
                    VehicleID = package.VehicleID
                };
                var data = db.Packages.Add(pack);
                int check = db.SaveChanges();
                if (check > 0) return 1;

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AddPackageModel> selectPackage()
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Packages.ToList();
                List<AddPackageModel> List = request.Select(x => new AddPackageModel
                {
                    Package_ID = x.Package_ID,
                    Picture = x.Picture,
                    Name = x.Name,
                    Pick_Up_Location = x.Pick_Up_Location,
                    No_of_Days = x.No_of_Days,
                    Price = x.Price,
                    Cities = x.Cities,
                    Places = x.Places,
                    Hotels = x.Hotels,
                    Inclusions = x.Inclusions,
                    Exclusions = x.Exclusions,
                    Date = (DateTime) x.Date,
                    No_of_Bookings = x.No_of_Bookings.Value,
                    Total_Seats = x.Total_Seats.Value,
                    IsCustomize = x.IsCustomize.Value,
                    //VehicleID = x.VehicleID.Value,
                    //GuiderID = x.GuiderID.Value,
                    //TripDays = x.TripDays,
                }).ToList();
                return List;
            }
        }

        public AddPackageModel Getpackage(int id)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Packages.FirstOrDefault(x => x.Package_ID == id);
                AddPackageModel List = new AddPackageModel
                {
                    Package_ID = request.Package_ID,
                    Name = request.Name,
                    Picture = request.Picture,
                    Pick_Up_Location = request.Pick_Up_Location,
                    Price = request.Price,
                    No_of_Days = request.No_of_Days,
                    No_of_Bookings = request.No_of_Bookings.Value,
                    Date = request.Date.Value,
                    DateString = request.Date.Value.ToShortDateString(),
                    Cities = request.Cities,
                    Places = request.Places,
                    TripDays = request.TripDays,
                    Hotels = request.Hotels,
                    Inclusions = request.Inclusions,
                    Exclusions = request.Exclusions,
                    Total_Seats = request.Total_Seats.Value,
                    IsCustomize = request.IsCustomize.Value,
                };
                return List;
            }
        }

        public AddCityModel GetCity(int id)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Citys.FirstOrDefault(x => x.City_ID == id);
                AddCityModel List = new AddCityModel
                {
                    City_ID = request.City_ID,
                    City_Name = request.City_Name,
                    City_Details = request.City_Details,
                    City_Picture1 = request.City_Picture1,
                    City_Picture2 = request.City_Picture2,
                    City_Picture3 = request.City_Picture3,
                    City_Picture4 = request.City_Picture4,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude
                };
                return List;
            }
        }

        public AddFamousPlaceModel GetPlace(int id)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.FamousPlaces.FirstOrDefault(x => x.Place_ID == id);
                AddFamousPlaceModel List = new AddFamousPlaceModel
                {
                    Place_ID = request.Place_ID,
                    City_ID = request.City_ID,
                    Place_Name = request.Place_Name,
                    Place_Details = request.Place_Details,
                    Place_Pic1 = request.Place_Pic1,
                    Place_Pic2 = request.Place_Pic2,
                    Place_Pic3 = request.Place_Pic3,
                    Place_Pic4 = request.Place_Pic4,
                };
                return List;
            }
        }
        
        public HotelModel GetHotel(int id)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Hotels.FirstOrDefault(x => x.Id == id);
                HotelModel List = new HotelModel
                {
                    Id = request.Id,
                    Name = request.Name,
                    Picture = request.Picture,
                    City_Id = request.City_Id,
                    Available_Rooms = request.Available_Rooms,
                    Rooms = request.Rooms,
                    Details = request.Details,
                    Price = request.Price.Value,
                    Rating = request.Rating,
                };
                return List;
            }
        }

        public List<AddCityModel> selectCity()
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Citys.ToList();
                List<AddCityModel> List = request.Select(x => new AddCityModel
                {
                    City_ID = x.City_ID,
                    City_Name = x.City_Name,
                    City_Details = x.City_Details,
                    City_Picture1 = x.City_Picture1,
                    City_Picture2 = x.City_Picture2,
                    City_Picture3 = x.City_Picture3,
                    City_Picture4 = x.City_Picture4,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude
                }).ToList();
                return List;
            }
        }
        
        public List<TourGuiderModel> selectGuider()
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.TourGuiders.ToList();
                List<TourGuiderModel> List = request.Select(x => new TourGuiderModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    Img = x.Img,
                    Age = x.Age.Value,
                    City = x.City,
                    State = x.State,
                }).ToList();
                return List;
            }
        }
        
        public List<GuiderReviewMapModel> selectGuider(string city)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.TourGuiders.Where(x=>x.City.ToLower().Contains(city.ToLower()) && x.Status == true).ToList();
                List<GuiderReviewMapModel> List = request.Select(x => new GuiderReviewMapModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    Img = x.Img,
                    Age = x.Age.Value,
                    City = x.City,
                    State = x.State,
                    Charge = x.Charge.Value,
                    Rating = db.GuiderReviews.Where(y => y.Guider_ID == x.ID).Average(y => y.Rating),
            }).ToList();
                return List;
            }
        }
        
        public List<VehicleModel> selectVehicle()
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Vehicles.Where(x => x.Status == true).ToList();
                List<VehicleModel> list = new List<VehicleModel>();
                foreach (var req in request) {
                    VehicleModel model = new VehicleModel();
                    model.Id = req.Id;
                    model.Vehicle_Company = req.Vehicle_Company;
                    model.Vehicle_Model = req.Vehicle_Model;
                    model.Vehicle_Model_Year = req.Vehicle_Model_Year;
                    model.Vehicle_Pictures = req.Vehicle_Pictures;
                    model.Vehicle_Type = req.Vehicle_Type;
                    model.Rent = req.Rent == null? 0: req.Rent.Value;
                    list.Add(model);
                }
                return list;
            }
        }
        
        public List<VehicleReviewMapModel> selectVehicle(string city)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Vehicles.Where(x => x.City.ToLower().Contains(city.ToLower()) && x.Status == true).ToList();
                List<VehicleReviewMapModel> list = new List<VehicleReviewMapModel>();
                foreach (var req in request) {
                    VehicleReviewMapModel model = new VehicleReviewMapModel();
                    model.Id = req.Id;
                    model.Name = req.User_Name;
                    model.Vehicle_Company = req.Vehicle_Company;
                    model.Vehicle_Model = req.Vehicle_Model;
                    model.Vehicle_Model_Year = req.Vehicle_Model_Year;
                    model.Vehicle_Pictures = req.Vehicle_Pictures;
                    model.Vehicle_Type = req.Vehicle_Type;
                    model.Rent = req.Rent == null? 0: req.Rent.Value;
                    model.Rating = db.VehicleReviews.Where(y => y.Vehicle_ID == req.Id).Average(y => y.Rating);
                    list.Add(model);
                }
                return list;
            }
        }

        public List<AddFamousPlaceModel> AllPlaces()
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.FamousPlaces.Include(x => x.City).ToList();
                List<AddFamousPlaceModel> List = request.Select(x => new AddFamousPlaceModel
                {
                    Place_ID = x.Place_ID,
                    City_ID = x.City_ID,
                    Place_Name = x.Place_Name,
                    Place_Details = x.Place_Details,
                    City_Name = x.City.City_Name,
                    Place_Pic1 = x.Place_Pic1,
                    Place_Pic2 = x.Place_Pic2,
                    Place_Pic3 = x.Place_Pic3,
                    Place_Pic4 = x.Place_Pic4,
                }).ToList();
                return List;
            }
        }
        
        public List<PlaceModel> selectPlace()
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.FamousPlaces.Include(x => x.City).ToList();
                List<PlaceModel> List = request.Select(x => new PlaceModel
                {
                    id = x.Place_ID,
                    city_id = x.City_ID,
                    place = x.Place_Name,
                    detail = x.Place_Details,
                    city = x.City.City_Name,
                    pic = x.Place_Pic1,
                    //Place_Pic2 = x.Place_Pic2,
                    //Place_Pic3 = x.Place_Pic3,
                    //Place_Pic4 = x.Place_Pic4,
                }).ToList();
                return List;
            }
        }
        public List<HotelMapModel> selectHotel()
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Hotels.Include(x => x.City).ToList();
                List<HotelMapModel> List = request.Select(x => new HotelMapModel
                {
                    Id = x.Id,
                    City_Id = x.City_Id,
                    City = x.City.City_Name,
                    Picture = x.Picture,
                    Details = x.Details,
                    Rating = x.Rating,
                    Price = x.Price.Value,
                    Name = x.Name,
                    Rooms = x.Rooms,
                    Available_Rooms = x.Available_Rooms
                }).ToList();

                return List;
            }
        }

        public int AddCity(AddCityModel addCity)
        {
            try
            {
                City city = new City()
                {
                    City_Name = addCity.City_Name,
                    City_Picture1 = addCity.City_Picture1,
                    City_Picture2 = addCity.City_Picture2,
                    City_Picture3 = addCity.City_Picture3,
                    City_Picture4 = addCity.City_Picture4,
                    City_Details = addCity.City_Details,
                    Latitude = addCity.Latitude,
                    Longitude = addCity.Longitude,
                };
                var data = db.Citys.Add(city);
                int check = db.SaveChanges();
                if (check > 0) return 1;

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int AddPlace(AddFamousPlaceModel addPlace)
        {
            try
            {
                FamousPlace place = new FamousPlace()
                {
                    City_ID = addPlace.City_ID,
                    Place_Name = addPlace.Place_Name,
                    Place_Details = addPlace.Place_Details,
                    Place_Pic1 = addPlace.Place_Pic1,
                    Place_Pic2 = addPlace.Place_Pic2,
                    Place_Pic3 = addPlace.Place_Pic3,
                    Place_Pic4 = addPlace.Place_Pic4,
                };
                var data = db.FamousPlaces.Add(place);
                int check = db.SaveChanges();
                if (check > 0) return 1;

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public int AddHotel(HotelModel hotelModel)
        {
            try
            {
                Hotel hotel = new Hotel()
                {
                    City_Id = hotelModel.City_Id,
                    Picture = hotelModel.Picture,
                    Name = hotelModel.Name,
                    Rating = hotelModel.Rating, 
                    Details = hotelModel.Details,   
                    Available_Rooms = hotelModel.Available_Rooms,
                    Rooms = hotelModel.Rooms,
                    Price = hotelModel.Price,
                };
                var data = db.Hotels.Add(hotel);
                int check = db.SaveChanges();
                if (check > 0) return 1;

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public int CompanyVehicle(VehicleModel vehicle)
        {
            try
            {
                Vehicle model = new Vehicle()
                {
                    User_Name = vehicle.User_Name,
                    Phone_No = vehicle.Phone_No,
                    Email = vehicle.Email,
                    Vehicle_Pictures = vehicle.Vehicle_Pictures,
                    Vehicle_No = vehicle.Vehicle_No,
                    Vehicle_Company = vehicle.Vehicle_Company,
                    Vehicle_Model = vehicle.Vehicle_Model,
                    Vehicle_Model_Year = vehicle.Vehicle_Model_Year,
                    Vehicle_Mileage = vehicle.Vehicle_Mileage,
                    Rent = vehicle.Rent,
                    City = vehicle.City,
                    Vehicle_Type = vehicle.Vehicle_Type,
                    Status = true,
                };
                var data = db.Vehicles.Add(model);
                int check = db.SaveChanges();
                if (check > 0) return 1;

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCity(AddCityModel cid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var Data = db.Citys.Where(x => x.City_ID == cid.City_ID).FirstOrDefault();
                if (Data != null)
                {
                    Data.City_ID = cid.City_ID;
                    //Data.City_Picture1 = cid.City_Picture1;
                    //Data.City_Picture2 = cid.City_Picture2;
                    //Data.City_Picture3 = cid.City_Picture3;
                    //Data.City_Picture4 = cid.City_Picture4;
                    Data.City_Name = cid.City_Name;
                    Data.City_Details = cid.City_Details;
                    Data.Latitude = cid.Latitude;
                    Data.Longitude = cid.Longitude;
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
        
        public bool UpdatePlace(AddFamousPlaceModel pid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var Data = db.FamousPlaces.Where(x => x.Place_ID == pid.Place_ID).FirstOrDefault();
                if (Data != null)
                {
                    Data.Place_ID = pid.Place_ID;
                    //Data.Place_Pic1 = pid.Place_Pic1;
                    //Data.Place_Pic2 = pid.Place_Pic2;
                    //Data.Place_Pic3 = pid.Place_Pic3;
                    //Data.Place_Pic4 = pid.Place_Pic4;
                    Data.City_ID = pid.City_ID;
                    Data.Place_Name = pid.Place_Name;
                    Data.Place_Details = pid.Place_Details;
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
        
        public bool UpdateHotel(HotelModel hid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var Data = db.Hotels.Where(x => x.Id == hid.Id).FirstOrDefault();
                if (Data != null)
                {
                    Data.Id = hid.Id;
                    Data.Name = hid.Name;
                    //Data.Picture = hid.Picture;
                    Data.Details = hid.Details;
                    Data.Rating = hid.Rating;
                    Data.City_Id = hid.City_Id;
                    Data.Rooms = hid.Rooms;
                    Data.Available_Rooms = hid.Available_Rooms;
                    Data.Price = hid.Price;
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
        
        public bool UpdatePackage(AddPackageModel packid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var Data = db.Packages.Where(x => x.Package_ID == packid.Package_ID).FirstOrDefault();
                if (Data != null)
                {
                    Data.Package_ID = packid.Package_ID;
                    //Data.Picture = packid.Picture;
                    Data.Name = packid.Name;
                    Data.Pick_Up_Location = packid.Pick_Up_Location;
                    Data.No_of_Days = packid.No_of_Days;
                    Data.Price = packid.Price;
                    Data.Date = packid.Date;
                    Data.Cities = packid.Cities;
                    Data.Places = packid.Places;
                    Data.TripDays = packid.TripDays;
                    Data.Hotels = packid.Hotels;
                    Data.Inclusions = packid.Inclusions;
                    Data.Exclusions = packid.Exclusions;
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

        public int GuiderJobApplication(TourGuiderModel gid)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                TourGuider tg = new TourGuider();
                tg.Name = gid.Name;
                tg.CNIC = gid.CNIC;
                tg.Contact_No = gid.Contact_No;
                tg.Licence_No = gid.Licence_No;
                tg.FilePath = gid.FilePath;
                tg.Img = gid.Img;
                tg.Age = gid.Age;
                tg.Address = gid.Address;
                tg.State = gid.State;
                tg.City = gid.City;
                tg.Status = false;
                db.TourGuiders.Add(tg);
                db.SaveChanges();

                guiderid = tg.ID;
            }
            return guiderid;
        }

        public int VehicleRentApplication(VehicleModel vehicleModel)
        {
            try
            {
                Vehicle vehicle = new Vehicle()
                {
                    User_Name = vehicleModel.User_Name,
                    Phone_No = vehicleModel.Phone_No,
                    Email = vehicleModel.Email,
                    Vehicle_Pictures = vehicleModel.Vehicle_Pictures,
                    Vehicle_No = vehicleModel.Vehicle_No,
                    Vehicle_Company = vehicleModel.Vehicle_Company,
                    Vehicle_Model = vehicleModel.Vehicle_Model,
                    Vehicle_Model_Year = vehicleModel.Vehicle_Model_Year,
                    Vehicle_Mileage = vehicleModel.Vehicle_Mileage,
                    City = vehicleModel.City,
                    Vehicle_Type = vehicleModel.Vehicle_Type,
                    Status = false,
                };
                var data = db.Vehicles.Add(vehicle);
                int check = db.SaveChanges();
                if (check > 0) return 1;

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BookingMapModel> GetBookingHistory(int id)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Bookings.OrderByDescending(y => y.BookingId).Where(x => x.U_Id == id).ToList();
                List<BookingMapModel> List = request.Select(x => new BookingMapModel{ 
                    bookingId = x.BookingId,
                    packageId = x.Package_ID,
                    isReviewed = x.isReviewed.Value,
                    packageName = db.Packages.FirstOrDefault(y => y.Package_ID == x.Package_ID).Name,
                    Date = (DateTime) db.Packages.FirstOrDefault(y => y.Package_ID == x.Package_ID).Date,
                    isCustomize = db.Packages.FirstOrDefault(y => y.Package_ID == x.Package_ID).IsCustomize.Value,
                }).ToList();
                return List;
            }
        }

        public SummaryModel Summary(int bookingid) {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var booking = db.Bookings.FirstOrDefault(x => x.BookingId == bookingid);
                var package = db.Packages.FirstOrDefault(x => x.Package_ID == booking.Package_ID);
                var transaction = db.Transactions.FirstOrDefault(x => x.Id == booking.Transaction_Id);
                var hotelArray = package.Hotels.Split(',');
                var hotel = string.Empty;
                foreach (var id in hotelArray)
                {
                    var hotelid = Convert.ToInt32(id);
                    hotel += $"{db.Hotels.FirstOrDefault(x => x.Id == hotelid).Name}, ";

                }
                hotel = hotel.Remove(hotel.Length - 2);

                var tax = Convert.ToInt32((Convert.ToInt32(transaction.Amount) * 0.02));
                var tickets = 0;
                if (package.IsCustomize == false) { 
                    tickets = (Convert.ToInt32(transaction.Amount) / package.Price);
                }
                else
                {
                    tickets = package.No_of_Bookings.Value;
                }
                var dateCheck = package.Date >= DateTime.Now;
                var status = string.Empty;
                if (dateCheck) {
                     status = "Valid";
                }
                else { 
                    status = "Expired";
                }

                SummaryModel summary = new SummaryModel()
                {
                    Name = booking.Name,
                    CNIC = booking.CNIC,
                    Contact_Number = booking.Contact_Number,
                    Address = booking.Address,
                    City = booking.City,
                    State = booking.State,
                    DOB = booking.DOB,
                    Package_Name = package.Name,
                    Pick_Up_Location = package.Pick_Up_Location,
                    No_of_Days = package.No_of_Days,
                    Price = package.Price,
                    Date = (DateTime)package.Date,
                    Hotels = hotel,
                    Card_Number = transaction.Card_Number,
                    Amount = transaction.Amount,
                    Tax = tax,
                    No_of_Tickets = tickets,
                    Status = status

                };
                return summary;
            }
        }

        public VehicleModel GetVehicle(int id)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.Vehicles.FirstOrDefault(x => x.Id == id);
                VehicleModel List = new VehicleModel
                {
                    User_Name = request.User_Name,
                    Phone_No = request.Phone_No,
                    Vehicle_No = request.Vehicle_No,
                    Vehicle_Company = request.Vehicle_Company,
                    Vehicle_Model = request.Vehicle_Model,
                    Vehicle_Model_Year = request.Vehicle_Model_Year,
                    Vehicle_Mileage = request.Vehicle_Mileage,
                    Vehicle_Pictures = request.Vehicle_Pictures,
                    Vehicle_Type = request.Vehicle_Type,
                };
                return List;
            }
        }
        
        public TourGuiderModel GetGuider(int id)
        {
            using (MoreToIt_DatabaseEntities db = new MoreToIt_DatabaseEntities())
            {
                var request = db.TourGuiders.FirstOrDefault(x => x.ID == id);
                TourGuiderModel List = new TourGuiderModel
                {
                    Name = request.Name,
                    Contact_No = request.Contact_No,
                    Licence_No = request.Licence_No,
                    Age = request.Age.Value,
                    Address = request.Address,
                    City = request.City,
                    State = request.State,
                    Img = request.Img,
                };
                return List;
            }
        }

    }
}