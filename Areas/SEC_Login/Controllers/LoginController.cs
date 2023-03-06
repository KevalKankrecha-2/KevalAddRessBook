using KevalThemeAddressBook.Areas.SEC_Login.Models;
using KevalThemeAddressBook.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KevalThemeAddressBook.Areas.SEC_Login.Controllers
{
    [Area("SEC_Login")]
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
        [HttpPost]
        public IActionResult LoginCheck(SEC_LoginModel modelSEC_Login)
        {
            DataTable dt;
            string error = null;
            if (modelSEC_Login.UserName == null)
            {
                error += "User Name is required";
            }
            if (modelSEC_Login.Password == null)
            {
                error += "<br/>Password is required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Index");
            }
            else
            {
                SEC_DAL dalSEC = new SEC_DAL();
                dt = dalSEC.SEC_LoginSelectByUserNamePassword(modelSEC_Login.UserName, modelSEC_Login.Password);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("Password", dr["UserPassword"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["LoginError"] = "User Name or Password is invalid!";
                    return View("Login");
                }
            }
            if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
            {
                return RedirectToAction("Index", "Home", new { area = "DashBoard" });
            }
            return View("Login");
        }
    }
}
