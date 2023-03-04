using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.Models
{
    public class MST_ContactCategoryModel
    {
        public int? ContactCategoryID { get; set; }

        public string ContactCategoryName { get; set; }
    }

    public class ContactCategoryDropDown
    {
        public int ContactCaregoryID { get; set; }
        [Required(ErrorMessage = "Please Enter Contact Category Name")]
        [StringLength(100, ErrorMessage = "Please Do not Enter State Name over 100 characters")]
        public string ContactCategoryName { get; set; }
    }
}
