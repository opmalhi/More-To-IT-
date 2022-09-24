using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class UserModel
    {
        public int U_Id { get; set; }
        public string Picture { get; set; }

        [Required(ErrorMessage ="Name is Required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Contact No is Required!")]
        public string Contact_Number { get; set; }
        [Required(ErrorMessage = "CNIC is Required!")]
        public string CNIC { get; set; }
        [Required(ErrorMessage = "Address is Required!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "State is Required!")]
        public string State { get; set; }
        [Required(ErrorMessage = "City is Required!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Date of Birth is Required!")]
        [Column(TypeName = "date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DOB { get; set; }
        //[Required(ErrorMessage = "Username is Required!")]
        //public string UserName { get; set; }
        [Required(ErrorMessage = "Password is Required!")]
        [MinLength(8, ErrorMessage = "Password Should Be Minimum Of 8 Character")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is Required!")]
        public string Email { get; set; }
        public int UserRole { get; set; }
        [Required(ErrorMessage = "Gender is Required!")]
        public string Gender { get; set; }


        public Boolean IsActive { get; set; }
    }
}