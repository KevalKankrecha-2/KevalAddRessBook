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

        #region Display CountryList
        public IActionResult Index()
        {
            LOC_DAL locDAL = new LOC_DAL();
            DataTable dtCountryList = locDAL.LOC_Country_SelectByUserID();
            return View("LOC_CountryList", dtCountryList);
        }
        #endregion

        #region OpenCountry Form
        public IActionResult Add(int? CountryID)
        {
            LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
            LOC_DAL locDAL = new LOC_DAL();

            if (CountryID != null)
            {
                DataTable dtCountry = locDAL.LOC_Country_SelectByPKUserID(CountryID);
                foreach (DataRow dr in dtCountry.Rows)
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
            LOC_DAL locDAL = new LOC_DAL();

            if (modelLOC_Country.CountryID == null)
            {
                String strmsg = locDAL.LOC_Country_InsertByUserID(modelLOC_Country);
                TempData["CountryMsg"] = "Country Inserted successfully.!";
            }
            else
            {
                string strmsg = locDAL.LOC_Country_UpdateByPKUserID(modelLOC_Country);
                TempData["CountryMsg"] = "Country Updated successfully.!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region CountryFilter
        public IActionResult LOC_CountrySearchByNameCode(string CountryName, string CountryCode)
        {
            LOC_DAL locDAL = new LOC_DAL();
            DataTable dtCountryFilterData = locDAL.LOC_Country_SelectByCountryNameCodeByUserID(CountryCode, CountryName);
            return View("LOC_CountryList", dtCountryFilterData);

           
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
            LOC_DAL locDAL = new LOC_DAL();
            locDAL.LOC_CountryDeleteByPKUserID(CountryID);
            TempData["CountryMsg"] = "Country Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion


    }
}