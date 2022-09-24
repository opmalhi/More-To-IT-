using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class GuiderReviewMapModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public int Age { get; set; }
        public int Charge { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<double> Rating { get; set; }
    }
}