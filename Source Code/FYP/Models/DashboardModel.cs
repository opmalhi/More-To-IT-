using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class DashboardModel
    {
        public string List { get; set; }
        public int total_users { get; set; }
        public int total_admins { get; set; }
        public int total_cities { get; set; }
        public int total_places { get; set; }
        public int total_hotels { get; set; }
        public int total_packages { get; set; }
        public int total_vehicle { get; set; }
        public int total_tourGuiderEmployee { get; set; }
        public int total_bookings { get; set; }
        public int total_pending { get; set; }
    }
}