using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "Email is Required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact No is Required!")]
        public string Phone_No { get; set; }

        [Required(ErrorMessage = "Vehicle No is Required!")]
        public string Vehicle_No { get; set; }

        [Required(ErrorMessage = "Vehicle Company is Required!")]
        public string Vehicle_Company { get; set; }

        [Required(ErrorMessage = "Vehicle Model is Required!")]
        public string Vehicle_Model { get; set; }

        [Required(ErrorMessage = "Vehicle Model Year is Required!")]
        public int Vehicle_Model_Year { get; set; }

        [Required(ErrorMessage = "Vehicle Mileage is Required!")]
        public int Vehicle_Mileage { get; set; }
        public int Rent { get; set; }
        public string Vehicle_Pictures { get; set; }

        [Required(ErrorMessage = "Vehicle Type is Required!")]
        public string Vehicle_Type { get; set; }
        
        [Required(ErrorMessage = "City is Required!")]
        public string City { get; set; }
        public bool Status { get; set; }

    }
}