using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class BookingMapModel
    {
        public int bookingId { get; set; }
        public int packageId { get; set; }
        public string packageName { get; set; }
        public DateTime Date { get; set; }

        public bool isCustomize { get; set; }
        public bool isReviewed { get; set; }
    }
}