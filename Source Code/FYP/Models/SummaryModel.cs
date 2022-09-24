using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class SummaryModel
    {
        public string Name { get; set; } 
        public string CNIC { get; set; } 
        public string Contact_Number { get; set; } 
        public DateTime DOB { get; set; } 
        public string Address { get; set; } 
        public string State { get; set; } 
        public string City { get; set; } 
        public string Package_Name { get; set; } 
        public string Pick_Up_Location { get; set; } 
        public string No_of_Days { get; set; } 
        public int Price { get; set; }

        [Column(TypeName = "date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } 
        public string Hotels { get; set; } 
        public string Card_Number { get; set; } 
        public string Amount { get; set; } 
        public int Tax { get; set; } 
        public int No_of_Tickets { get; set; } 
        public string Status { get; set; } 

        
    }
}