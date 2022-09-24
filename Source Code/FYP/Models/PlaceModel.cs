using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class PlaceModel
    {
        public int id { get; set; }
        public int city_id { get; set; }
        public string place { get; set; }
        public string pic { get; set; }
        public string detail { get; set; }
        public string city { get; set; }
    }
}