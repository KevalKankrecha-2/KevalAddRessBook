
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
        LOC_DAL ObjLocDal = new LOC_DAL();

        private IConfiguration Configuration;

        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region Display CountryList
        public IActionResult Index()
        {
            string ConnectionString= this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = ObjLocDal.LOC_Country_SelectAll(ConnectionString,UserID);
            return View("LOC_CountryList", dt);
        }
        #endregion

        #region OpenCountry Form
        public IActionResult OpenPage(int? CountryID)
        {
            LOC_CountryModel ObjModelCountry = new LOC_CountryModel();
            if (CountryID != null)
            {
                string ConnectionString= this.Configuration.GetConnectionString("myConnectionString");
                DataTable dt=ObjLocDal.LOC_Country_SelectByPK(ConnectionString, CountryID, UserID);
                foreach (DataRow dr in dt.Rows)
                {
                    ObjModelCountry.CountryID = Convert.ToInt32(dr["CountryID"]);
                    ObjModelCountry.CountryName = Convert.ToString(dr["CountryName"]);
                    ObjModelCountry.CountryCode = Convert.ToString(dr["CountryCode"]);
                }
            }
            return View("LOC_CountryAddEdit", ObjModelCountry);
        }
        #endregion

        #region Perform Add Edit in Country
        [HttpPost]
        public IActionResult Save(LOC_CountryModel Obj_ModelLOC_Country)
        {
            string ConnectionString= this.Configuration.GetConnectionString("myConnectionString");
            if (Obj_ModelLOC_Country.CountryID == null)
            {
                String strmsg=ObjLocDal.LOC_Country_Insert(ConnectionString,UserID, Obj_ModelLOC_Country);
                TempData["CountryMsg"] = "Country Inserted successfully.!";
            }
            else
            {
                string strmsg= ObjLocDal.LOC_Country_UpdateByPK(ConnectionString, UserID,Obj_ModelLOC_Country);
                TempData["CountryMsg"] = "Country Updated successfully.!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region CountryFilter
        public IActionResult LOC_CountrySearchByNameCode(string CountryName, string CountryCode)
        {
            string ConnectionString= this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = ObjLocDal.LOC_Country_SelectByCountryNameCode(ConnectionString, CountryCode, CountryName,UserID);
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
            string ConnectionString= this.Configuration.GetConnectionString("myConnectionString");
            ObjLocDal.DeleteBYPK(ConnectionString, UserID, "PR_LOC_Country_DeleteByPK", "CountryID", CountryID);
            TempData["CountryMsg"] = "Country Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion

        
    }
}
