using KevalThemeAddressBook.Areas.LOC_Country.Models;
using KevalThemeAddressBook.BAL;
using KevalThemeAddressBook.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace KevalThemeAddressBook.Areas.LOC_Country.Controllers
{
    [CheckAccess]
    [Area("LOC_Country")]
    public class LOC_CountryController : Controller
    {
        LOC_DAL locDAL = new LOC_DAL();


        #region Display CountryList
        public IActionResult Index()
        {
            DataTable dt = locDAL.LOC_Country_SelectAll();
            return View("LOC_CountryList", dt);
        }
        #endregion

        #region OpenCountry Form
        public IActionResult OpenPage(int? CountryID)
        {
            LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
            if (CountryID != null)
            {
                DataTable dt = locDAL.LOC_Country_SelectByPK(CountryID);
                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_Country.CountryName = Convert.ToString(dr["CountryName"]);
                    modelLOC_Country.CountryCode = Convert.ToString(dr["CountryCode"]);
                }
            }
            return View("LOC_CountryAddEdit", modelLOC_Country);
        }
        #endregion

        #region Perform Add Edit in Country
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            if (modelLOC_Country.CountryID == null)
            {
                String strmsg = locDAL.LOC_Country_Insert(modelLOC_Country);
                TempData["CountryMsg"] = "Country Inserted successfully.!";
            }
            else
            {
                string strmsg = locDAL.LOC_Country_UpdateByPK(modelLOC_Country);
                TempData["CountryMsg"] = "Country Updated successfully.!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region CountryFilter
        public IActionResult LOC_CountrySearchByNameCode(string CountryName, string CountryCode)
        {
            DataTable dt = locDAL.LOC_Country_SelectByCountryNameCode(CountryCode, CountryName);
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
            locDAL.LOC_CountryDeleteByPK(CountryID);
            TempData["CountryMsg"] = "Country Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion


    }
}