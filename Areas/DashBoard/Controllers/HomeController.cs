using KevalThemeAddressBook.BAL;
using KevalThemeAddressBook.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Threading.Tasks;

namespace KevalThemeAddressBook.Areas.DashBoard.Controllers
{
    [CheckAccess]
    [Area("DashBoard")]
    public class HomeController : Controller
    {

        private IConfiguration Configuration;

        public HomeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        DashBoard_DAL dalDashBorad = new DashBoard_DAL();
        public IActionResult Index()
        {
            DataTable dt = dalDashBorad.LOC_CountryCount();
            ViewBag.CountryCount = dt.Rows[0]["country"];

            dt = dalDashBorad.LOC_StateCount();
            ViewBag.StateCount = dt.Rows[0]["state"];

            dt = dalDashBorad.LOC_CityCount();
            ViewBag.cityCount = dt.Rows[0]["cities"];

            dt = dalDashBorad.CON_ContactCount();
            ViewBag.contactcount = dt.Rows[0]["contact"];

            dt = dalDashBorad.MST_ContactCategoryCount();
            ViewBag.contactcategorycount = dt.Rows[0]["contactcategory"];
            return View("Index");
        }
    }
}
