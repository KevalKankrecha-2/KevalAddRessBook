using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using KevalThemeAddressBook.Models;
using KevalThemeAddressBook.Areas.LOC_Country.Models;
using KevalThemeAddressBook.Areas.LOC_State.Models;
using KevalThemeAddressBook.DAL;

namespace KevalThemeAddressBook.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    public class LOC_StateController : Controller
    {
        List<LOC_CountryDropDownModel> CountryDropDownList = new List<LOC_CountryDropDownModel>();
      
        LOC_DAL dalLOC = new LOC_DAL();

        #region open State Form
        public IActionResult OpenPage(int? StateID)
        {
            LOC_StateModel modelLOC_State = new LOC_StateModel();
            if (StateID != null)
            {
                DataTable ObjDt = dalLOC.LOC_State_SelectByPK(StateID);
                foreach (DataRow dr in ObjDt.Rows)
                {
                    modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_State.StateName = Convert.ToString(dr["StateName"]);
                    modelLOC_State.StateCode = Convert.ToString(dr["StateCode"]);
                    modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                }
            }

            //Here Country Drop Down are Passed For Add/Edit Mode
            DataTable dt = dalLOC.LOC_Country_SelectForDropDown();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_CountryDropDownModel CountryDropDown = new LOC_CountryDropDownModel();
                CountryDropDown.CountryID = (int)dr["CountryID"];
                CountryDropDown.CountryName = (string)dr["CountryName"];
                CountryDropDownList.Add(CountryDropDown);
            }
            ViewBag.CountryList = CountryDropDownList;

            return View("LOC_StateAddEdit", modelLOC_State);
            //here statemodel pass for display value which we will update
        }
        #endregion

        #region Load StateList
        public IActionResult Index()
        {
            DataTable dt = dalLOC.LOC_State_SelectAll();


            /*To pass country drop down for filter in state list */
            DataTable dt1 = dalLOC.LOC_Country_SelectForDropDown(); ;
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel CountryDropDown = new LOC_CountryDropDownModel();
                CountryDropDown.CountryID = (int)dr1["CountryID"];
                CountryDropDown.CountryName = (string)dr1["CountryName"];
                CountryDropDownList.Add(CountryDropDown);
            }
            ViewBag.CountryList = CountryDropDownList;
            /*end*/
            return View("LOC_StateList", dt);
        }
        #endregion

        #region Perform Add Edit 
        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            if (modelLOC_State.StateID == null)
            {
                string strmsg = dalLOC.LOC_State_Insert(modelLOC_State);
                TempData["StateMsg"] = "State Inserted successfully.!";
            }
            else
            {
                string strmessage = dalLOC.LOC_State_UpdateByPK(modelLOC_State);
                TempData["StateMsg"] = "State Updated successfully.!";
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            dalLOC.LOC_StateDeleteByPK(StateID);
            TempData["StateMsg"] = "State Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion

        #region State Filter
        public IActionResult LOC_StateSearchByCountryIDStateNameCode(int CountryID, string StateName, string StateCode)
        {
            DataTable dt = dalLOC.LOC_State_SelectByStateNameCode(CountryID, StateName, StateCode);

            /*To pass country drop down for filter in state list */
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
            return View("LOC_StateList", dt);

        }
        #endregion

        #region Cancel Add/Edit
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
        #endregion
    }
}
