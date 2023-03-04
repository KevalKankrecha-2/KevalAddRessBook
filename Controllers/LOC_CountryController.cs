
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
            LOC_DAL locdal = new LOC_DAL();
            string strcon = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = locdal.LOC_Country_SelectAll(strcon,UserID);
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
                LOC_DAL locdal = new LOC_DAL();
                DataTable dt=locdal.LOC_Country_SelectByPK(strcon, CountryID, UserID);
               
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
            LOC_DAL locdal = new LOC_DAL();
            if (modelLOC_Country.CountryID == null)
            {
                String strmsg=locdal.LOC_Country_Insert(str,UserID, modelLOC_Country);
                TempData["CountryMsg"] = "Country Inserted successfully.!";
            }
            else
            {
                string strmsg= locdal.LOC_Country_UpdateByPK(str, UserID,modelLOC_Country);
                TempData["CountryMsg"] = "Country Updated successfully.!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region CountryFilter
        public IActionResult LOC_CountrySearchByNameCode(string CountryName, string CountryCode)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locdal = new LOC_DAL();
            DataTable dt = locdal.LOC_Country_SelectByCountryNameCode(str, CountryCode, CountryName,UserID);
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
            LOC_DAL locdal = new LOC_DAL();
            locdal.DeleteBYPK(str, UserID, "PR_LOC_Country_DeleteByPK", "CountryID", CountryID);
            TempData["CountryMsg"] = "Country Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion

        
    }
}
