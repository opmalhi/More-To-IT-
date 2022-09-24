using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class TourGuiderModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="User Name is Required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "CNIC is Required!")]
        public string CNIC { get; set; }

        [Required(ErrorMessage ="Contact No is Required!")]
        public string Contact_No { get; set; }

        [Required(ErrorMessage ="Email is Required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Licence No is Required!")]
        public string Licence_No { get; set; }

        public bool Status { get; set; }

        public string FilePath { get; set; }

        public string Img { get; set; }

        [Required(ErrorMessage = "Age is Required!")]
        public int Age { get; set; }
        
        [Required(ErrorMessage = "Address is Required!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is Required!")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is Required!")]
        public string State { get; set; }
        public int Rating { get; set; }

        //public HttpPostedFile Resume { get; set; }
    }
}