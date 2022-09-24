using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class CityMapModel
    {
        public string City_Name { get; set; }
        public string City_Details { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string StayDays { get; set; }

        public string City_Picture1 { get; set; }
        public string City_Picture2 { get; set; }
        public string City_Picture3 { get; set; }
        public string City_Picture4 { get; set; }
        public List<PlaceMapModel> placeMapModels { get; set; }
    }
}