using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class AddCityModel
    {
        [DisplayName("ID")]
        public int City_ID { get; set; }
        [Required(ErrorMessage ="Name Is Required!")]
        [DisplayName("Name")]
        public string City_Name { get; set; }
        [Required(ErrorMessage = "Details Is Required!")]
        [DisplayName("Details")]
        public string City_Details { get; set; }
        [Required(ErrorMessage = "Latitude Is Required!")]
        public string Latitude { get; set; }
        [Required(ErrorMessage = "Longitude Is Required!")]
        public string Longitude { get; set; }
        public string City_Picture1 { get; set; }
        public string City_Picture2 { get; set; }
        public string City_Picture3 { get; set; }
        public string City_Picture4 { get; set; }
    }
}