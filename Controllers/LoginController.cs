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
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        private IConfiguration Configuration;

        public LoginController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult LoginCheck(string UserName,string Password)
        {
            string uname="", upass="";
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_User_Master_SelectByNamePassword";
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@UserPassword", Password);

            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            int a = dt.Rows.Count;
            if (a == 1)
            {
                DataRow dr = dt.Rows[0];
                ViewBag.UserID = dr["UserID"];
                return RedirectToAction("Index", "Home");
            }
            else
                ViewBag.LoginErrorMSG = "Please Enter Correct User Name and Password";
                return View("Login");
        }
    }
}
