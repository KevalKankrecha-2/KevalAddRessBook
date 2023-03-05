using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.Areas.LOC_City.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }
        public int StateID { get; set; }

        [Required(ErrorMessage = "Please Enter City Name")]
        [StringLength(100, ErrorMessage = "Please Do not Enter City Name over 100 characters")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Please Enter City Name")]
        public string CityCode { get; set; }

        public int CountryID { get; set; }

    }
    public class LOC_CityDropDown
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }
}
