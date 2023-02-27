using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.Data;
using System.Data.SqlClient;
using System.Linq;

using System.Threading.Tasks;

namespace KevalThemeAddressBook.Controllers
{
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
            cmd.CommandText = "PR_Selecyt_Count_Country";
            int userID = 1;
            cmd.Parameters.AddWithValue("@UserID", userID);
            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            foreach(DataRow dr in dt.Rows)
            {
                ViewBag.CountryCount = dr["country"];
            }

            cmd.CommandText = "PR_Select_Count_State";
            SqlDataReader sdrstate = cmd.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(sdrstate);
            foreach (DataRow dr in dt1.Rows)
            {
                ViewBag.StateCount = dr["state"];
            }

            cmd.CommandText = "PR_Selecyt_Count_City";
            SqlDataReader sdrcity = cmd.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(sdrcity);
            foreach (DataRow dr in dt2.Rows)
            {
                ViewBag.cityCount = dr["cities"];
            }


            cmd.CommandText = "PR_Select_Count_Contacts";
            SqlDataReader sdrcontact = cmd.ExecuteReader();
            DataTable dt3 = new DataTable();
            dt3.Load(sdrcontact);
            foreach (DataRow dr in dt3.Rows)
            {
                ViewBag.contactcount = dr["contact"];
            }



            cmd.CommandText = "PR_Select_Count_Contacts_Category";
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
