using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.Models
{
    public class LOC_ContactModel
    {
        public int? ContactID { get; set; }

        [Required(ErrorMessage = "Please Enter Contact Name")]
        [StringLength(100, ErrorMessage = "Please Do not Enter State Name over 100 characters")]
        public string ContactName { get; set; }
        public int CountryID { get; set; }

        public int StateID { get; set; }

        public int CityID { get; set; }

        public int ContactCategoryID { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Number Name")]
        [Phone(ErrorMessage ="Please Enter Proper Mobile Number")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please Enter Whatsapp Number Name")]
        [Phone(ErrorMessage = "Please Enter Proper Whatsapp Number")]
        public string WhatsappNo { get; set; }

        [Required(ErrorMessage = "Please Select BirthDate")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress(ErrorMessage ="Enter Proper Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Age Address")]

        public int Age { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Blood Group")]
        public string BloodGroup { get; set; }

        [Required(ErrorMessage = "Please Enter FaceBook Id")]
        public string FaceBookID { get; set; }

        [Required(ErrorMessage = "Please Enter Instagram ID")]
        public string InstaID { get; set; }

        public IFormFile File { get; set; }

        public string PhotoPath { get; set; }
    }

    public class ContactDropDown
    {
        public int CountryID { get; set; }

        public string CountryName { get; set; }

        public int StateID { get; set; }

        public string StateName { get; set; }

        public int CityID { get; set; }

        public string CityName { get; set; }
    }
}
