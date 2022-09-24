using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FYP.Models
{
    public class CreateAdminModel
    {
        public int User_ID { get; set; }

        [Required(ErrorMessage = "Kindly Write a User's Full Name?")]
        public string Full_Name { get; set; }

        [Required(ErrorMessage = "Kindly Write a User's Father Name?")]
        public string Father_Name { get; set; }

        [Required]
        [MaxLength(13, ErrorMessage = "Enter a valid NIC of 13 Digits")]
        [MinLength(13, ErrorMessage = "Enter a valid NIC of 13 Digits")]
        public string NIC { get; set; }

        [Required(ErrorMessage = "Kindly Select a Gender?")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Kindly Enter a Date of Birth?")]
        [Column(TypeName = "date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage = "Enter a valid Contact Format: 03#########")]
        [MinLength(11, ErrorMessage = "Enter a valid contact Format: 03#########")]
        public string Contact_Number { get; set; }

        [Required(ErrorMessage = "Kindly Enter a Address?")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Kindly Enter a City?")]
        public string City { get; set; }

        [Required(ErrorMessage = "Kindly Enter a State?")]
        public string State { get; set; }

        [Required(ErrorMessage = "Kindly Enter a User Name?")]
        public string UserName { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password Should Be Minimum Of 8 Character")]
        public string Password { get; set; }

        public string Picture { get; set; }

        public int UserRole { get; set; }
        public bool IsActive { get; set; }

        public LoginModel logininfo { get; set; }
    }
}