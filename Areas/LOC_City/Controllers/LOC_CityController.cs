using Microsoft.AspNetCore.Mvc;
using KevalThemeAddressBook.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Collections.Generic;
using KevalThemeAddressBook.Areas.LOC_Country.Models;
using KevalThemeAddressBook.Areas.LOC_State.Models;
using KevalThemeAddressBook.Areas.LOC_City.Models;
using System;
using KevalThemeAddressBook.DAL;
using KevalThemeAddressBook.BAL;

namespace KevalThemeAddressBook.Areas.LOC_City.Controllers
{
    [CheckAccess]
    [Area("LOC_City")]
    public class LOC_CityController : Controller
    {

        #region Open CityForm
        List<LOC_CountryDropDownModel> CountryDropDownList = new List<LOC_CountryDropDownModel>();
        List<LOC_StateDropDown> StateDropDownList = new List<LOC_StateDropDown>();
        LOC_DAL dalLOC = new LOC_DAL();
        public IActionResult OpenPage(int? CityID)
        {
            LOC_CityModel modelLOC_City = new LOC_CityModel();

            #region Country Pass As Drop Down in Edit/Add Mode
            DataTable dt = dalLOC.LOC_Country_SelectForDropDown();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_CountryDropDownModel CountryDropDown = new LOC_CountryDropDownModel();
                CountryDropDown.CountryID = Convert.ToInt32(dr["CountryID"]);
                CountryDropDown.CountryName = Convert.ToString(dr["CountryName"]);
                CountryDropDownList.Add(CountryDropDown);
            }
            ViewBag.CountryList = CountryDropDownList;
            #endregion

            #region Select By PK
            if (CityID != null)
            {
                DataTable dtupdt = dalLOC.LOC_City_SelectByPK(CityID);
                foreach (DataRow drupdt in dtupdt.Rows)
                {
                    modelLOC_City.CityID = Convert.ToInt32(drupdt["CityID"]);
                    modelLOC_City.CityName = Convert.ToString(drupdt["CityName"]);
                    modelLOC_City.CityCode = Convert.ToString(drupdt["CityCode"]);
                    modelLOC_City.CountryID = Convert.ToInt32(drupdt["CountryID"]);
                    modelLOC_City.StateID = Convert.ToInt32(drupdt["StateID"]);
                }
            }
            #endregion

            #region in edit mode to display previous selected state by country
            if (CityID != null)
            {
                LOC_DAL dalLOC = new LOC_DAL();
                dt = dalLOC.dbo_PR_LOC_State_SelectDropDownByCountryID(modelLOC_City.CountryID);
                foreach (DataRow dr in dt.Rows)
                {
                    LOC_StateDropDown StateDropDown = new LOC_StateDropDown();
                    StateDropDown.StateID = Convert.ToInt32(dr["StateID"]);
                    StateDropDown.StateName = Convert.ToString(dr["StateName"]);
                    StateDropDownList.Add(StateDropDown);
                }
                ViewBag.StateList = StateDropDownList;
            }
            else
            {
                List<LOC_StateDropDown> statedropdown = new List<LOC_StateDropDown>();
                ViewBag.StateList = statedropdown;
            }
            #endregion

            return View("LOC_CityAddEdit", modelLOC_City);
        }
        #endregion

        #region Perform Add Edit
        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            if (modelLOC_City.CityID == null)
            {
               string strmsg= dalLOC.LOC_City_Insert(modelLOC_City);
                TempData["CityMsg"] = "City Inserted successfully.!";
            }
            else
            {
                dalLOC.LOC_City_UpdateByPK(modelLOC_City);
                TempData["CityMsg"] = "City Updated successfully.!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Load City List
        public IActionResult Index()
        {

            DataTable dt = dalLOC.LOC_City_SelectAll();

            /*To pass country drop down for filter in City list */
            DataTable dt1 = dalLOC.LOC_Country_SelectForDropDown();
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel CountryDropDown = new LOC_CountryDropDownModel();
                CountryDropDown.CountryID = (int)dr1["CountryID"];
                CountryDropDown.CountryName = (string)dr1["CountryName"];
                CountryDropDownList.Add(CountryDropDown);
            }
            ViewBag.CountryList = CountryDropDownList;
            /*end*/

            return View("LOC_CityList", dt);
        }
        #endregion

        #region Delete City
        public IActionResult Delete(int CityID)
        {
            dalLOC.LOC_CityDeleteBYPK(CityID);
            TempData["CityMsg"] = "City Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion

        #region Cancel Add/Edit
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
        #endregion

        #region Casceding Drop dowm
        [HttpPost]
        public IActionResult DropdownByCountryID(int CountryID)
        {
            DataTable dt=dalLOC.dbo_PR_LOC_State_SelectDropDownByCountryID(CountryID);
            foreach(DataRow dr in dt.Rows)
            {
                LOC_StateDropDown dropdown = new LOC_StateDropDown();
                dropdown.StateID = Convert.ToInt32(dr["StateID"]);
                dropdown.StateName = dr["StateName"].ToString();
                StateDropDownList.Add(dropdown);
            }
            var vModel = StateDropDownList;
            return Json(vModel);
        }
        #endregion

        #region CityFilter
        public IActionResult CityFilter(int CountryID,int StateID,string CityName,string CityCode)
        {
            DataTable dt = dalLOC.LOC_City_SelectByCityNameCode(CountryID, StateID, CityName, CityCode);


           /*To pass country drop down for filter in City list */
            DataTable dt1 = dalLOC.LOC_Country_SelectForDropDown();
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel CountryDropDown = new LOC_CountryDropDownModel();
                CountryDropDown.CountryID = (int)dr1["CountryID"];
                CountryDropDown.CountryName = (string)dr1["CountryName"];
                CountryDropDownList.Add(CountryDropDown);
            }
            ViewBag.CountryList = CountryDropDownList;
            /*end*/

            return View("LOC_CityList", dt);
        }
        #endregion
    }
}
