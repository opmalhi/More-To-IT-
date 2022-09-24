using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class VehicleReviewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public string Email { get; set; }
        public int Vehicle_ID { get; set; }
    }
}