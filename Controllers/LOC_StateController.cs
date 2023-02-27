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
                SelectByPkDAL selectbypkdal = new SelectByPkDAL();
                DataTable dtupt = selectbypkdal.SelectByPk(strcon, UserID, "PR_Loc_State_SelectBYPK", "@StateID", StateID);
                
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
            DeleteSelectAll_DAL daldeleteselectall = new DeleteSelectAll_DAL();
            DataTable dt = daldeleteselectall.SelectAll(strcon, UserID, "PR_LOC_State_SelectAll");
            /*To pass country drop down for filter in state list */
            DropDown_DAL dropdowndal = new DropDown_DAL();
            DataTable dt1 = dropdowndal.DropDown(strcon, UserID, "PR_LOC_Country_SelectForDropDownList"); ;
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
            //LOC_StateModel statemode = new LOC_StateModel();
            string str = this.Configuration.GetConnectionString("myConnectionString");
            if (modelLOC_State.StateID == null)
            {
                Insert_DAL insDAL = new Insert_DAL();
                String strmessage = insDAL.Insert_State(str, UserID, "PR_LOC_State_Insert", modelLOC_State.StateName, modelLOC_State.StateCode, modelLOC_State.CountryID);
            }
            else
            {
                Update_DAL insDAL = new Update_DAL();
                String strmessage = insDAL.Update_State(str, UserID, "PR_LOC_State_UpdateByPK", modelLOC_State.StateName, modelLOC_State.StateCode, modelLOC_State.CountryID,modelLOC_State.StateID);
            }
            return RedirectToAction("Index");

        }
        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DeleteSelectAll_DAL daldeleteselectall = new DeleteSelectAll_DAL();
            daldeleteselectall.DeleteBYPK(str, UserID, "PR_LOC_State_DeleteByPK", "StateID", StateID);
            return RedirectToAction("Index");
        }
        #endregion

        #region State Filter
        public IActionResult LOC_StateSearchByCountryIDStateNameCode(int CountryID,string StateName,string StateCode)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand objcmd = conn.CreateCommand();
            objcmd.CommandType = CommandType.StoredProcedure;
            objcmd.CommandText = "PR_LOC_State_Filter";
            objcmd.Parameters.AddWithValue("@UserID", UserID);
            if (CountryID == 0)
            {
                objcmd.Parameters.AddWithValue("@CountryID", DBNull.Value);
            }
            else
            {
                ViewBag.FilterCountryID = CountryID;
                objcmd.Parameters.AddWithValue("@CountryID", CountryID);
            }
            if (StateName == null)
            {
                objcmd.Parameters.AddWithValue("@StateName", DBNull.Value);
            }
            else
            {
                ViewBag.FilterStateName = StateName;
                objcmd.Parameters.AddWithValue("@StateName", StateName);
            }
            if (StateCode == null)
            {
                objcmd.Parameters.AddWithValue("@StateCode", DBNull.Value);
            }
            else
            {
                ViewBag.FilterStateCode = StateCode;
                objcmd.Parameters.AddWithValue("@StateCode", StateCode);
            }
            SqlDataReader objSDR = objcmd.ExecuteReader();
            dt.Load(objSDR);

            /*To pass country drop down for filter in state list */
            DropDown_DAL dropdowndal = new DropDown_DAL();
            DataTable dt1 = dropdowndal.DropDown(str, UserID, "PR_LOC_Country_SelectForDropDownList"); ;
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
