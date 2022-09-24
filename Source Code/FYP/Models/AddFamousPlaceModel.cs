using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class AddFamousPlaceModel
    {
        public int Place_ID { get; set; }
        [Required(ErrorMessage = "City Is Required!")]
        public int City_ID { get; set; }
        [Required(ErrorMessage = "Place Name Is Required!")]
        public string Place_Name { get; set; }
        public string Place_Pic1 { get; set; }
        public string Place_Pic2 { get; set; }
        public string Place_Pic3 { get; set; }
        public string Place_Pic4 { get; set; }
        [Required(ErrorMessage = "Details Is Required!")]
        public string Place_Details { get; set; }
        public string City_Name { get; set; }
    }
}