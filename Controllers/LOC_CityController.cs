using Microsoft.AspNetCore.Mvc;
using KevalThemeAddressBook.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Collections.Generic;
using System;
using KevalThemeAddressBook.DAL;

namespace KevalAddressBook.Controllers
{
   
    public class LOC_CityController : Controller
    {
        int UserID = 1;
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region Open CityForm
        List<LOC_CountryDropDownModel> countrydropdown = new List<LOC_CountryDropDownModel>();
        List<LOC_StateDropDown> statedropdownlist = new List<LOC_StateDropDown>();
        
        public IActionResult OpenPage(int? CityID)
        {
            LOC_DAL locdal = new LOC_DAL();
            LOC_CityModel citymodel = new LOC_CityModel();
            if (CityID != null)
            {
                string strcon = this.Configuration.GetConnectionString("myConnectionString");
                DataTable dtupdt = locdal.LOC_City_SelectByPK(strcon, CityID, UserID);


                foreach (DataRow drupdt in dtupdt.Rows)
                {
                    citymodel.CityID = Convert.ToInt32(drupdt["CityID"]);
                    citymodel.CityName = Convert.ToString(drupdt["CityName"]);
                    citymodel.CityCode = Convert.ToString(drupdt["CityCode"]);
                    citymodel.CountryID = Convert.ToInt32(drupdt["CountryID"]);
                    citymodel.StateID = Convert.ToInt32(drupdt["StateID"]);
                }
            }

            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = locdal.LOC_Country_SelectForDropDown(str,UserID);
            foreach (DataRow dr in dt.Rows)
            {
                LOC_CountryDropDownModel dropdowncountry = new LOC_CountryDropDownModel();
                dropdowncountry.CountryID = Convert.ToInt32(dr["CountryID"]);
                dropdowncountry.CountryName = Convert.ToString(dr["CountryName"]);
                countrydropdown.Add(dropdowncountry);
            }
            ViewBag.CountryList = countrydropdown;


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
                cmd.Parameters.AddWithValue("@CountryID", citymodel.CountryID);
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
            #endregion

            else
            {
                List<LOC_StateDropDown> statedropdown = new List<LOC_StateDropDown>();
                ViewBag.StateList = statedropdown;
            }
           
            return View("LOC_CityAddEdit",citymodel);
        }
        #endregion

        #region Perform Add Edit
        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locdal = new LOC_DAL();
            if (modelLOC_City.CityID == null)
            {
               string strmsg= locdal.LOC_City_Insert(str, UserID, modelLOC_City);
            }
            else
            {
                locdal.LOC_City_UpdateByPK(str, UserID,modelLOC_City);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Load City List
        public IActionResult Index()
        {

            string strcon = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locdal = new LOC_DAL();
            DataTable dt = locdal.LOC_City_SelectAll(strcon, UserID);

            /*To pass country drop down for filter in City list */
            SqlConnection conn = new SqlConnection(strcon);
            DataTable dt1 = locdal.LOC_Country_SelectForDropDown(strcon, UserID);
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel dropdown = new LOC_CountryDropDownModel();
                dropdown.CountryID = (int)dr1["CountryID"];
                dropdown.CountryName = (string)dr1["CountryName"];
                countrydropdown.Add(dropdown);
            }
            ViewBag.CountryList = countrydropdown;
            /*end*/

            return View("LOC_CityList", dt);
        }
        #endregion

        #region Delete City
        public IActionResult Delete(int CityID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locdal = new LOC_DAL();
            locdal.DeleteBYPK(str, UserID, "PR_LOC_City_DeleteByPK", "CityID", CityID);
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
            LOC_DAL locdal = new LOC_DAL();
            DataTable dt=locdal.dbo_PR_LOC_State_SelectDropDownByCountryID(str,CountryID,UserID);
            List<LOC_StateDropDown> statedropdown = new List<LOC_StateDropDown>();
            foreach(DataRow dr in dt.Rows)
            {
                LOC_StateDropDown dropdown = new LOC_StateDropDown();
                dropdown.StateID = Convert.ToInt32(dr["StateID"]);
                dropdown.StateName = dr["StateName"].ToString();
                statedropdown.Add(dropdown);
            }
            var vModel = statedropdown;
            return Json(vModel);
        }
        #endregion

        #region CityFilter
        public IActionResult CityFilter(int CountryID,int StateID,string CityName,string CityCode)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locdal = new LOC_DAL();
            DataTable dt = locdal.LOC_City_SelectByCityNameCode(str, CountryID, StateID, CityName, CityCode, UserID);


           /*To pass country drop down for filter in City list */
            SqlConnection conn = new SqlConnection(str);
            
            DataTable dt1 = locdal.LOC_Country_SelectForDropDown(str, UserID);
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel dropdown = new LOC_CountryDropDownModel();
                dropdown.CountryID = (int)dr1["CountryID"];
                dropdown.CountryName = (string)dr1["CountryName"];
                countrydropdown.Add(dropdown);
            }
            ViewBag.CountryList = countrydropdown;
            /*end*/

            return View("LOC_CityList", dt);
        }
        #endregion
    }
}
