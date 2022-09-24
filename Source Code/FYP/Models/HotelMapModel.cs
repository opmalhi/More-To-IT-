using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class HotelMapModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Details { get; set; }
        public int Rating { get; set; }
        public int City_Id { get; set; }
        public string City { get; set; }
        public int Rooms { get; set; }
        public int Price { get; set; }
        public int Available_Rooms { get; set; }
    }
}