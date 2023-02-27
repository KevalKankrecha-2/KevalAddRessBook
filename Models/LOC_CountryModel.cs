﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }

        [Required(ErrorMessage ="Please Enter Country Name")]
        [StringLength(100,ErrorMessage ="Please Do not Enter Country Name over 100 characters")]
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Please Enter Country Code")]
        public string CountryCode { get; set; }   
    }

    public class LOC_CountryDropDownModel
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }

}
