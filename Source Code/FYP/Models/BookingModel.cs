using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class BookingModel
    {
       
            public int Id { get; set; }

            [Required(ErrorMessage ="Name Is Required!")]
            public string Name { get; set; }

            [Required(ErrorMessage ="Email Is Required!")]
            public string Email { get; set; }

            [Required(ErrorMessage ="Contact_No Is Required!")]
            public string Contact_Number { get; set; }

            [Required(ErrorMessage ="CNIC Is Required!")]
            public string CNIC { get; set; }

            [Required(ErrorMessage ="Date of Birth Is Required!")]
            public DateTime DOB { get; set; }

            [Required(ErrorMessage ="Address Is Required!")]
            public string Address { get; set; }

            [Required(ErrorMessage ="State Is Required!")]
            public string State { get; set; }

            [Required(ErrorMessage ="City Is Required!")]
            public string City { get; set; }
            public int No_of_Tickets { get; set; }

            public int PackageId { get; set; }
            public string HotelId { get; set; }
        
            [Required(ErrorMessage ="Holder Name Is Required!")]
            public string CardHolderName { get; set; }

            [Required(ErrorMessage ="Card No Is Required!")]
            public string CardNumber { get; set; }

            [Required(ErrorMessage = "Expire Month Is Required!")]
            public int Month { get; set; }

            [Required(ErrorMessage ="Expire Year Is Required!")]
            public int Year { get; set; }

            [Required(ErrorMessage ="CVC Is Required!")]
            public string CVC { get; set; }
            public int Amount { get; set; }

            public bool isReviewed { get; set; } = false;
    }
}