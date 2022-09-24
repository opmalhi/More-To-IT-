using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class AddPackageModel
    {
        public int Package_ID { get; set; }

        public string Picture { get; set; }

        [Required(ErrorMessage = "Kindly Write a Package Name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kindly Write a Pick Up Location Name!")]
        public string Pick_Up_Location { get; set; }

        [Required(ErrorMessage = "Enter No Of Days!")]
        public string No_of_Days { get; set; }

        [Required(ErrorMessage = "Enter Trip Days!")]
        public string TripDays { get; set; }

        [Required(ErrorMessage = "Enter Price!")]
        public int Price { get; set; }

        [Column(TypeName = "date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string DateString { get; set; }

        [Required]
        public string Cities { get; set; }
        [Required]
        public string Places { get; set; }
        [Required]
        public string Hotels { get; set; }
        [Required]
        public string Inclusions { get; set; }
        [Required]
        public string Exclusions { get; set; }
        public int No_of_Bookings { get; set; }
        [Required(ErrorMessage = "Total No of Seats Is Required!")]
        public int Total_Seats { get; set; }
        public bool IsCustomize { get; set; }

        public int GuiderID { get; set; }
        public int VehicleID { get; set; }

    }
}