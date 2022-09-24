using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class HotelModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hotel Name Is Required!")]
        public string Name { get; set; }
        public string Picture { get; set; }

        [Required(ErrorMessage = "Hotel Details Is Required!")]
        public string Details { get; set; }

        [Required(ErrorMessage = "Rating Is Required!")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "City Is Required!")]
        public int City_Id { get; set; }

        public int Rooms { get; set; }

        [Required(ErrorMessage = "Price of Room Is Required!")]
        public int Price { get; set; }
        public int Available_Rooms { get; set; }
    }
}