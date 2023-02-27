
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using KevalThemeAddressBook.Models;
using KevalThemeAddressBook.DAL;

namespace KevalThemeAddressBook.Controllers
{

    public class LOC_CountryController : Controller
    {
        int UserID = 1;

        private IConfiguration Configuration;

        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region Display CountryList
        public IActionResult Index()
        {
            string strcon = this.Configuration.GetConnectionString("myConnectionString");
            DeleteSelectAll_DAL daldeleteselectall = new DeleteSelectAll_DAL();
            DataTable dt = daldeleteselectall.SelectAll(strcon,UserID, "PR_LOC_Country_SelectAll");
            return View("LOC_CountryList", dt);
        }
        #endregion

        #region OpenCountry Form
        public IActionResult OpenPage(int? CountryID)
        {
            LOC_CountryModel modelcountry = new LOC_CountryModel();
            if (CountryID != null)
            {
                string strcon = this.Configuration.GetConnectionString("myConnectionString");
                SelectByPkDAL selectbypkdal = new SelectByPkDAL();
                DataTable dt=selectbypkdal.SelectByPk(strcon, UserID, "PR_LOC_Country_SelectByPK","@CountryID", CountryID);
               
                foreach (DataRow dr in dt.Rows)
                {
                    modelcountry.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelcountry.CountryName = Convert.ToString(dr["CountryName"]);
                    modelcountry.CountryCode = Convert.ToString(dr["CountryCode"]);
                }
            }
            return View("LOC_CountryAddEdit", modelcountry);
        }
        #endregion
        #region Perform Add Edit in Country
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            if (modelLOC_Country.CountryID == null)
            {
                Insert_DAL insDAL = new Insert_DAL();
                String strmessage=insDAL.Insert_Country(str,UserID, "PR_LOC_Country_Insert", modelLOC_Country.CountryName, modelLOC_Country.CountryCode);
            }
            else
            {
                Update_DAL uptdal = new Update_DAL();
                string strmsg= uptdal.Update_Country(str, UserID, "PR_LOC_Country_UpdateByPK", modelLOC_Country.CountryName, modelLOC_Country.CountryCode, modelLOC_Country.CountryID);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region CountryFilter
        public IActionResult LOC_CountrySearchByNameCode(string CountryName, string CountryCode)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_Country_Filter";
            objcmd.Parameters.AddWithValue("@UserID", UserID);

            if (CountryName == null)
            {
                objcmd.Parameters.AddWithValue("@CountryName", DBNull.Value);
            }
            else
            {
                ViewBag.FilterCountryName=CountryName;
                objcmd.Parameters.AddWithValue("@CountryName", CountryName);
            }
            if (CountryCode == null)
            {
                objcmd.Parameters.AddWithValue("@CountryCode", DBNull.Value);
            }
            else
            {
                ViewBag.FilterCountryCode = CountryCode;
                objcmd.Parameters.AddWithValue("@CountryCode", CountryCode);
            }


            SqlDataReader objSDR = objcmd.ExecuteReader();
            dt.Load(objSDR);
            conn.Close();
            return View("LOC_CountryList", dt);
        }
        #endregion

        #region Cancel Add/Edit
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
        #endregion


        #region Delete Country
        public IActionResult Delete(int CountryID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DeleteSelectAll_DAL daldeleteselectall = new DeleteSelectAll_DAL();
            daldeleteselectall.DeleteBYPK(str, UserID, "PR_LOC_Country_DeleteByPK", "CountryID", CountryID);
            return RedirectToAction("Index");
        }
        #endregion

        
    }
}
