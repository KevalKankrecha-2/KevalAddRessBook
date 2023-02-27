using Microsoft.AspNetCore.Mvc;
using KevalThemeAddressBook.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Web;
using System.Collections.Generic;
using System;
using System.IO;
using KevalThemeAddressBook.DAL;

namespace KevalThemeAddressBook.Controllers
{

    public class ContactController : Controller
    {
        private IConfiguration Configuration;
        public ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        int UserID = 1;
        LOC_ContactModel modelcontact = new LOC_ContactModel();
        List<LOC_CountryDropDownModel> countrydropdown = new List<LOC_CountryDropDownModel>();
        List<LOC_StateDropDown> statedropdown = new List<LOC_StateDropDown>();
        List<LOC_CityDropDown> citydropdown = new List<LOC_CityDropDown>();
        List<ContactCategoryDropDown> contactcategirydropdown = new List<ContactCategoryDropDown>();

        #region Open Contact Form
        public IActionResult OpenPage(int? ContactID)
        {
            LOC_StateModel statemodel = new LOC_StateModel();
            DropDown_DAL dropdowndal = new DropDown_DAL();
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dropdowndal.DropDown(str, UserID, "PR_LOC_Country_SelectForDropDownList");
            foreach (DataRow dr in dt.Rows)
            {
                LOC_CountryDropDownModel dropdown = new LOC_CountryDropDownModel();
                dropdown.CountryID = (int)dr["CountryID"];
                dropdown.CountryName = (string)dr["CountryName"];
                countrydropdown.Add(dropdown);
            }
            ViewBag.CountryList = countrydropdown;
            

            //for contact category Drop Down
            DataTable dtccddd = dropdowndal.DropDown(str, UserID, "PR_ContactCategory_SelectForDropDownList");
            foreach (DataRow dr in dtccddd.Rows)
            {
                ContactCategoryDropDown dropdown = new ContactCategoryDropDown();
                dropdown.ContactCaregoryID = (int)dr["ContactCategoryID"];
                dropdown.ContactCategoryName = (string)dr["ContactCategoryName"];
                contactcategirydropdown.Add(dropdown);
            }
            ViewBag.ContactCategoryDropDownList = contactcategirydropdown;

            //end Contact Category Drop Down



            // data render for update
            if (ContactID != null)
            {

                string strcon = this.Configuration.GetConnectionString("myConnectionString");
                SelectByPkDAL selectbypkdal = new SelectByPkDAL();
                DataTable dtupdt = selectbypkdal.SelectByPk(strcon, UserID, "PR_Contact_SelectByPK", "@ContactID", ContactID);


                foreach (DataRow drupt in dtupdt.Rows)
                {
                    modelcontact.ContactID = Convert.ToInt32(drupt["ContactID"]);
                    modelcontact.ContactName = Convert.ToString(drupt["ContactName"]);
                    modelcontact.ContactCategoryID = Convert.ToInt32(drupt["ContectCategoryID"]);
                    modelcontact.ContactNo = Convert.ToString(drupt["ContactNo"]);
                    modelcontact.WhatsappNo = Convert.ToString(drupt["WhatsappNo"]);
                    modelcontact.Email = Convert.ToString(drupt["Email"]);
                    modelcontact.Address = Convert.ToString(drupt["Address"]);
                    modelcontact.FaceBookID = Convert.ToString(drupt["FaceBookID"]);
                    modelcontact.InstaID = Convert.ToString(drupt["InstaID"]);
                    modelcontact.CountryID = Convert.ToInt32(drupt["CountryID"]);
                    modelcontact.StateID = Convert.ToInt32(drupt["StateID"]);
                    modelcontact.CityID = Convert.ToInt32(drupt["CityID"]);
                    modelcontact.Age = Convert.ToInt32(drupt["Age"]);
                    modelcontact.BirthDate = Convert.ToDateTime(drupt["BirthDate"]);
                    modelcontact.BloodGroup = Convert.ToString(drupt["BloodGroup"]);
                    ViewBag.EditImageURL= Convert.ToString(drupt["PhotoPath"]);
                    modelcontact.PhotoPath= Convert.ToString(drupt["PhotoPath"]);
                }

                // State Pass Here
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                List<LOC_StateDropDown> stdropdown = new List<LOC_StateDropDown>();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Loc_State_SelectDropDownByCountryID";
                cmd.Parameters.AddWithValue("@CountryID", modelcontact.CountryID);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                SqlDataReader sdr =  cmd.ExecuteReader();
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

                //CityList Passed Here
                List<LOC_CityDropDown> citydropdownlistedit = new List<LOC_CityDropDown>();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_Loc_State_SelectDropDownByStateID";
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelcontact.StateID;
                sdr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(sdr);
                foreach (DataRow dr in dt.Rows)
                {
                    LOC_CityDropDown dropdowncity = new LOC_CityDropDown();
                    dropdowncity.CityID = Convert.ToInt32(dr["CityID"]);
                    dropdowncity.CityName = Convert.ToString(dr["CityName"]);
                    citydropdownlistedit.Add(dropdowncity);
                }
                ViewBag.CityList = citydropdownlistedit;
            }
            else
            {
                List<LOC_StateDropDown> statedropdown = new List<LOC_StateDropDown>();
                ViewBag.StateList = statedropdown;
                List<LOC_CityDropDown> citydropdownlist = new List<LOC_CityDropDown>();
                ViewBag.CityList = citydropdownlist;
            }
            
            return View("ContactAddEdit", modelcontact);
        }
        #endregion

        #region Load Contacts
        public IActionResult Index()
        {
            string strcon = this.Configuration.GetConnectionString("myConnectionString");
            DeleteSelectAll_DAL daldeleteselectall = new DeleteSelectAll_DAL();
            DataTable dt = daldeleteselectall.SelectAll(strcon, UserID, "PR_Contact_SelectAllRecord");

            /*To pass country drop down for filter in Contact list */
            SqlConnection conn = new SqlConnection(strcon);
            DropDown_DAL dropdowndal = new DropDown_DAL();
            DataTable dt1 = dropdowndal.DropDown(strcon, UserID, "PR_LOC_Country_SelectForDropDownList");
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel dropdown = new LOC_CountryDropDownModel();
                dropdown.CountryID = (int)dr1["CountryID"];
                dropdown.CountryName = (string)dr1["CountryName"];
                countrydropdown.Add(dropdown);
            }
            ViewBag.CountryList = countrydropdown;
            /*end*/
            return View("ContactList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DeleteSelectAll_DAL daldeleteselectall = new DeleteSelectAll_DAL();
            daldeleteselectall.DeleteBYPK(str, UserID, "PR_Contact_Delete",  "ContactID", ContactID);
            return RedirectToAction("Index");
        }
        #endregion

        #region Add Edit Contacts
        public IActionResult Save(LOC_ContactModel modelLOC_Contact)
        {
            if (modelLOC_Contact.File != null)
            {
                string FilePath = "wwwroot\\upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(),FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filenameWithPath = Path.Combine(path, modelLOC_Contact.File.FileName);
                modelLOC_Contact.PhotoPath = "~"+FilePath.Replace("wwwroot\\","/")+"/"+modelLOC_Contact.File.FileName;
                using (var stream = new FileStream(filenameWithPath, FileMode.Create))
                {
                    modelLOC_Contact.File.CopyTo(stream);
                }
                
            }
            string str = this.Configuration.GetConnectionString("myConnectionString");
            if (modelLOC_Contact.ContactID == null)
            {
                Insert_DAL insDAL = new Insert_DAL();
                String strmessage = insDAL.Insert_Contact(str, UserID, "PR_Contact_Insert", modelLOC_Contact.ContactName, modelLOC_Contact.CountryID, modelLOC_Contact.StateID, modelLOC_Contact.CityID, modelLOC_Contact.ContactCategoryID, modelLOC_Contact.ContactNo, modelLOC_Contact.WhatsappNo, modelLOC_Contact.BirthDate, modelLOC_Contact.Email, modelLOC_Contact.Age, modelLOC_Contact.Address, modelLOC_Contact.BloodGroup, modelLOC_Contact.FaceBookID, modelLOC_Contact.InstaID, modelLOC_Contact.PhotoPath);
            }
            else
            {
                Update_DAL uptdal = new Update_DAL();
                string strmsg=uptdal.Update_Contact(str, UserID, "PR_Contact_UpdateByPK", modelLOC_Contact.ContactName, modelLOC_Contact.CountryID, modelLOC_Contact.StateID, modelLOC_Contact.CityID, modelLOC_Contact.ContactCategoryID, modelLOC_Contact.ContactNo, modelLOC_Contact.WhatsappNo, modelLOC_Contact.BirthDate, modelLOC_Contact.Email, modelLOC_Contact.Age, modelLOC_Contact.Address, modelLOC_Contact.BloodGroup, modelLOC_Contact.FaceBookID, modelLOC_Contact.InstaID, modelLOC_Contact.PhotoPath, modelLOC_Contact.ContactID); 
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Cancel Add/Edit
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
        #endregion

        #region Casceding Dropdown
        [HttpPost]
        public IActionResult DropdownByCountryID(int CountryID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            DropDown_DAL dropdowndal = new DropDown_DAL();
            DataTable dt=dropdowndal.Casceding_DropDown(str,UserID, "PR_Loc_State_SelectDropDownByCountryID", "CountryID", CountryID);
            conn.Close();
            List<LOC_StateDropDown> statedropdown = new List<LOC_StateDropDown>();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_StateDropDown dropdown = new LOC_StateDropDown();
                dropdown.StateID = Convert.ToInt32(dr["StateID"]);
                dropdown.StateName = dr["StateName"].ToString();
                statedropdown.Add(dropdown);
            }
            var vModel = statedropdown;
            return Json(vModel);
        }

        public IActionResult DropdownByStateID(int StateID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DropDown_DAL dropdowndal = new DropDown_DAL();
            DataTable dt=dropdowndal.Casceding_DropDown(str,UserID, "PR_Loc_State_SelectDropDownByStateID", "StateID", StateID);
            List<LOC_CityDropDown> citydropdown = new List<LOC_CityDropDown>();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_CityDropDown dropdown = new LOC_CityDropDown();
                dropdown.CityID = Convert.ToInt32(dr["CityID"]);
                dropdown.CityName = dr["CityName"].ToString();
                citydropdown.Add(dropdown);
            }
            var vModel = citydropdown;
            return Json(vModel);
        }
        #endregion

        #region ContactFilter
        public IActionResult Contact_Filter(int CountryID,int StateID,int CityID,string ContactName)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Contact_Filter";
            cmd.Parameters.AddWithValue("@UserID", UserID);
            if (CountryID == 0)
            {
                cmd.Parameters.AddWithValue("@CountryID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CountryID", CountryID);
            }
            if (StateID == 0)
            {
                cmd.Parameters.AddWithValue("@StateID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@StateID", StateID);
            }
            if (CityID == 0)
            {
                cmd.Parameters.AddWithValue("@CityID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CityID", CityID);
            }
            if(ContactName == null)
            {
                cmd.Parameters.AddWithValue("@ContactName", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ContactName", ContactName);
            }
            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);


            /*To pass country drop down for filter in Contact list */
            DropDown_DAL dropdowndal = new DropDown_DAL();
            DataTable dt1 = dropdowndal.DropDown(str, UserID, "PR_LOC_Country_SelectForDropDownList");
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel dropdown = new LOC_CountryDropDownModel();
                dropdown.CountryID = (int)dr1["CountryID"];
                dropdown.CountryName = (string)dr1["CountryName"];
                countrydropdown.Add(dropdown);
            }
            ViewBag.CountryList = countrydropdown;
            /*end*/

            return View("ContactList",dt);
        }
        #endregion
    }
}