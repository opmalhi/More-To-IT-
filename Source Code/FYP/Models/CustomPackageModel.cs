using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class CustomPackageModel
    {
        public string City { get; set; }
        public int People { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int U_Id { get; set; }

        public string Name { get; set; }
        public string Contact_No { get; set; }

        [Column(TypeName = "date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        public string CNIC { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string U_City { get; set; }

        public string packName { get; set; }
        public string pickUp { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }

        public string Cities { get; set; }
        public string Places { get; set; }
        public string Hotels { get; set; }
        public string No_of_Days { get; set; }
        public int No_of_Tickets { get; set; }
        public string TripDays { get; set; }
        public int Guider { get; set; }
        public int Vehicle { get; set; }
        public string HolderName { get; set; }
        public string CardNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string CVV { get; set; }
        public int Amount { get; set; }
        
    }
}