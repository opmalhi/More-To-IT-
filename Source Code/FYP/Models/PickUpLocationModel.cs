using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class PickUpLocationModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "City Name Is Required!")]
        public string City { get; set; }
    }
}