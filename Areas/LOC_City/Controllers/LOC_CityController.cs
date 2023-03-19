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
        public IActionResult Add(int? CityID)
        {
            LOC_CityModel modelLOC_City = new LOC_CityModel();

            #region Country Pass As Drop Down in Edit/Add Mode
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dtCountryDropDownList = dalLOC.LOC_Country_SelectForDropDownListByUserID();
            List<LOC_CountryDropDownModel> CountryDropDownList = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dtCountryDropDownList.Rows)
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
                DataTable dtCity = dalLOC.LOC_City_SelectByPKUserID(CityID);
                foreach (DataRow drupdt in dtCity.Rows)
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
                DataTable dtStateDropDownList = dalLOC.LOC_State_SelectDropDownByCountryIDUserID(modelLOC_City.CountryID);
                List<LOC_StateDropDown> StateDropDownList = new List<LOC_StateDropDown>();
                foreach (DataRow dr in dtStateDropDownList.Rows)
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
            LOC_DAL dalLOC = new LOC_DAL();
            if (modelLOC_City.CityID == null)
            {
               string strmsg= dalLOC.LOC_City_InsertByUserID(modelLOC_City);
                TempData["CityMsg"] = "City Inserted successfully.!";
            }
            else
            {
                dalLOC.LOC_City_UpdateByPKUserID(modelLOC_City);
                TempData["CityMsg"] = "City Updated successfully.!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Load City List
        public IActionResult Index()
        {
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dtCityList = dalLOC.LOC_City_SelectByUserID();

            /*To pass country drop down for filter in City list */
            DataTable dtCountryDropDownList = dalLOC.LOC_Country_SelectForDropDownListByUserID();
            List<LOC_CountryDropDownModel> CountryDropDownList = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr1 in dtCountryDropDownList.Rows)
            {
                LOC_CountryDropDownModel CountryDropDown = new LOC_CountryDropDownModel();
                CountryDropDown.CountryID = (int)dr1["CountryID"];
                CountryDropDown.CountryName = (string)dr1["CountryName"];
                CountryDropDownList.Add(CountryDropDown);
            }
            ViewBag.CountryList = CountryDropDownList;
            /*end*/

            return View("LOC_CityList", dtCityList);
        }
        #endregion

        #region Delete City
        public IActionResult Delete(int CityID)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            dalLOC.LOC_CityDeleteBYPKUserID(CityID);
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
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt=dalLOC.LOC_State_SelectDropDownByCountryIDUserID(CountryID);
            List<LOC_StateDropDown> StateDropDownList = new List<LOC_StateDropDown>();
            foreach (DataRow dr in dt.Rows)
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
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.LOC_City_SelectByCityNameCodeUserID(CountryID, StateID, CityName, CityCode);


           /*To pass country drop down for filter in City list */
            DataTable dt1 = dalLOC.LOC_Country_SelectForDropDownListByUserID();
            List<LOC_CountryDropDownModel> CountryDropDownList = new List<LOC_CountryDropDownModel>();
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
