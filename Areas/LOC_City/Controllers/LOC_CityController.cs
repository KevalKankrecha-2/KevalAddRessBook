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

namespace KevalThemeAddressBook.Areas.LOC_City.Controllers
{

    [Area("LOC_City")]
    public class LOC_CityController : Controller
    {
        int UserID = 1;
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region Open CityForm
        List<LOC_CountryDropDownModel> CountryDropDownList = new List<LOC_CountryDropDownModel>();
        List<LOC_StateDropDown> StateDropDownList = new List<LOC_StateDropDown>();
        LOC_DAL dalLOC = new LOC_DAL();
        public IActionResult OpenPage(int? CityID)
        {
            LOC_CityModel modelLOC_City = new LOC_CityModel();

            #region Country Pass As Drop Down in Edit/Add Mode
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalLOC.LOC_Country_SelectForDropDown(str, UserID);
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
                string strcon = this.Configuration.GetConnectionString("myConnectionString");
                DataTable dtupdt = dalLOC.LOC_City_SelectByPK(strcon, CityID, UserID);
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
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                List<LOC_StateDropDown> stdropdown = new List<LOC_StateDropDown>();
                cmd.CommandText = "PR_LOC_State_SelectStateDropDownByCountryID";
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@CountryID", modelLOC_City.CountryID);
                SqlDataReader sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                foreach (DataRow dr in dt.Rows)
                {
                    LOC_StateDropDown dropdownstate = new LOC_StateDropDown();
                    dropdownstate.StateID = Convert.ToInt32(dr["StateID"]);
                    dropdownstate.StateName = Convert.ToString(dr["StateName"]);
                    stdropdown.Add(dropdownstate);
                }
                ViewBag.StateList = stdropdown;
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
            string str = this.Configuration.GetConnectionString("myConnectionString");
            
            if (modelLOC_City.CityID == null)
            {
               string strmsg= dalLOC.LOC_City_Insert(str, UserID, modelLOC_City);
                TempData["CityMsg"] = "City Inserted successfully.!";
            }
            else
            {
                dalLOC.LOC_City_UpdateByPK(str, UserID,modelLOC_City);
                TempData["CityMsg"] = "City Updated successfully.!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Load City List
        public IActionResult Index()
        {

            string strcon = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalLOC.LOC_City_SelectAll(strcon, UserID);

            /*To pass country drop down for filter in City list */
            SqlConnection conn = new SqlConnection(strcon);
            DataTable dt1 = dalLOC.LOC_Country_SelectForDropDown(strcon, UserID);
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
            string str = this.Configuration.GetConnectionString("myConnectionString");
            dalLOC.DeleteBYPK(str, UserID, "PR_LOC_City_DeleteByPK", "CityID", CityID);
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
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt=dalLOC.dbo_PR_LOC_State_SelectDropDownByCountryID(str,CountryID,UserID);
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
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalLOC.LOC_City_SelectByCityNameCode(str, CountryID, StateID, CityName, CityCode, UserID);


           /*To pass country drop down for filter in City list */
            SqlConnection conn = new SqlConnection(str);
            DataTable dt1 = dalLOC.LOC_Country_SelectForDropDown(str, UserID);
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
