using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class ChoicesDropdownForPlace
    {
        public int id { get; set; }
        public string label { get; set; }
        public string value { get; set; }
        public bool selected { get; set; } = false;
        //public bool disabled { get; set; } = false;
    }
}