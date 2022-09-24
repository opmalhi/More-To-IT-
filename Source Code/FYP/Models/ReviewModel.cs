using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class ReviewModel
    {
        public int packid { get; set; }
        public int bookingid { get; set; }
        public int packRating { get; set; } = 0;
        public string packReview { get; set; } = String.Empty;
        public int guiderRating { get; set; } = 0;
        public string guiderReview { get; set; } = String.Empty;
        public int vehicleRating { get; set; } = 0;
        public string vehicleReview { get; set; } = String.Empty;

        public int U_Id { get; set; }
    }
}