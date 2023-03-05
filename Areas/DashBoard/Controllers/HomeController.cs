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
    [Area("DashBoard")]
    public class HomeController : Controller
    {

        private IConfiguration Configuration;

        public HomeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult Index()
        { 

        string strcon = this.Configuration.GetConnectionString("myConnectionString");
        SqlConnection con = new SqlConnection(strcon);
        con.Open();
        SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_SelectCount";
            int userID = 1;
            cmd.Parameters.AddWithValue("@UserID", userID);
            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            foreach(DataRow dr in dt.Rows)
            {
                ViewBag.CountryCount = dr["country"];
            }

            cmd.CommandText = "PR_LOC_State_SelectCount";
            SqlDataReader sdrstate = cmd.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(sdrstate);
            foreach (DataRow dr in dt1.Rows)
            {
                ViewBag.StateCount = dr["state"];
            }

            cmd.CommandText = "PR_LOC_City_SelectCount";
            SqlDataReader sdrcity = cmd.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(sdrcity);
            foreach (DataRow dr in dt2.Rows)
            {
                ViewBag.cityCount = dr["cities"];
            }


            cmd.CommandText = "PR_CON_Contact_SelectCount";
            SqlDataReader sdrcontact = cmd.ExecuteReader();
            DataTable dt3 = new DataTable();
            dt3.Load(sdrcontact);
            foreach (DataRow dr in dt3.Rows)
            {
                ViewBag.contactcount = dr["contact"];
            }



            cmd.CommandText = "PR_MST_ContactCategory_SelectCount";
            SqlDataReader sdrcontactcaregory = cmd.ExecuteReader();
            DataTable dt4 = new DataTable();
            dt4.Load(sdrcontactcaregory);
            foreach (DataRow dr in dt4.Rows)
            {
                ViewBag.contactcategorycount = dr["contactcategory"];
            }
            return View("Index");
        }
    }
}
