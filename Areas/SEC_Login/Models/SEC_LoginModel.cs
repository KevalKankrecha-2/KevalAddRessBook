using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KevalThemeAddressBook.Areas.SEC_Login.Models
{
    public class SEC_LoginModel
    {
        public int UserID { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
