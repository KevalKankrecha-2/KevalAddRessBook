using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using KevalThemeAddressBook.Models;
using KevalThemeAddressBook.DAL;

namespace KevalAddressBook.Controllers
{
    public class LOC_StateController : Controller
    {
        List<LOC_CountryDropDownModel> countrydropdown = new List<LOC_CountryDropDownModel>();
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        int UserID = 1;

        #region open State Form
        public IActionResult OpenPage(int? StateID)
        {
            LOC_StateModel statemodel = new LOC_StateModel();
            if (StateID != null)
            {
                string strcon = this.Configuration.GetConnectionString("myConnectionString");
                LOC_DAL locdal = new LOC_DAL();
                DataTable dtupt = locdal.LOC_State_SelectByPK(strcon, StateID, UserID);
                
                foreach(DataRow dr in dtupt.Rows)
                {
                    statemodel.StateID = Convert.ToInt32(dr["StateID"]);
                    statemodel.StateName = Convert.ToString(dr["StateName"]);
                    statemodel.StateCode = Convert.ToString(dr["StateCode"]);
                    statemodel.CountryID = Convert.ToInt32(dr["CountryID"]);
                }
            }
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_SelectForDropDownList";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            

            foreach(DataRow dr in dt.Rows)
            {
                LOC_CountryDropDownModel dropdown = new LOC_CountryDropDownModel();
                dropdown.CountryID = (int)dr["CountryID"];
                dropdown.CountryName = (string)dr["CountryName"];
                countrydropdown.Add(dropdown);
            }
            ViewBag.CountryList = countrydropdown;
            return View("LOC_StateAddEdit",statemodel);
            //here statemodel pass for display value which we will update
        }
        #endregion

        #region Load StateList
        public IActionResult Index()
        {
            string strcon = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locdal = new LOC_DAL();
            DataTable dt = locdal.LOC_State_SelectAll(strcon, UserID);


            /*To pass country drop down for filter in state list */
            DataTable dt1 = locdal.LOC_Country_SelectForDropDown(strcon, UserID); ;
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel dropdown = new LOC_CountryDropDownModel();
                dropdown.CountryID = (int)dr1["CountryID"];
                dropdown.CountryName = (string)dr1["CountryName"];
                countrydropdown.Add(dropdown);
            }
            ViewBag.CountryList = countrydropdown;
            /*end*/
            return View("LOC_StateList", dt);
        }
        #endregion
        
        #region Perform Add Edit 
        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locdal = new LOC_DAL();
            if (modelLOC_State.StateID == null)
            {
               string strmsg= locdal.LOC_State_Insert(str, UserID, modelLOC_State);
            }
            else
            {
                string strmessage = locdal.LOC_State_UpdateByPK(str, UserID, modelLOC_State);
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locdal = new LOC_DAL();
            locdal.DeleteBYPK(str, UserID, "PR_LOC_State_DeleteByPK", "StateID", StateID);
            return RedirectToAction("Index");
        }
        #endregion

        #region State Filter
        public IActionResult LOC_StateSearchByCountryIDStateNameCode(int CountryID,string StateName,string StateCode)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locdal = new LOC_DAL();
            DataTable dt = locdal.LOC_State_SelectByStateNameCode(str,CountryID,StateName, StateCode,UserID);

            /*To pass country drop down for filter in state list */
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
