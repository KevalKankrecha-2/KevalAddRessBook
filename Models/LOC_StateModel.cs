using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }


        [Required(ErrorMessage ="Please Select Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Please Enter State Name")]
        [StringLength(100, ErrorMessage = "Please Do not Enter State Name over 100 characters")]
        public string StateName { get; set; }

        [Required(ErrorMessage = "Please Enter State Code")]
        public string StateCode { get; set; }
    }
    public class LOC_StateDropDown
    {
       public int StateID { get; set; }

        public string StateName { get; set; }
    }
}