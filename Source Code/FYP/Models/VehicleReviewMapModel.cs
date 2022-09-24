using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class VehicleReviewMapModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vehicle_Company { get; set; }
        public string Vehicle_Model { get; set; }
        public int Vehicle_Model_Year { get; set; }
        public string Vehicle_Pictures { get; set; }
        public string Vehicle_Type { get; set; }
        public int Rent { get; set; }
        public Nullable<double> Rating { get; set; }
    }
}