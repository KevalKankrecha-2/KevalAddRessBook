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
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL locdal = new LOC_DAL();
            DataTable dt= locdal.LOC_Country_SelectForDropDown(str, UserID);
            foreach (DataRow dr in dt.Rows)
            {
                LOC_CountryDropDownModel dropdown = new LOC_CountryDropDownModel();
                dropdown.CountryID = (int)dr["CountryID"];
                dropdown.CountryName = (string)dr["CountryName"];
                countrydropdown.Add(dropdown);
            }
            ViewBag.CountryList = countrydropdown;


            //for contact category Drop Down
              Contact_Category_DAL contactcatdal = new Contact_Category_DAL();
          
            DataTable dtccddd = contactcatdal.ContactCategory_DropDownList(str, UserID);
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
                CON_DAL condal = new CON_DAL();
                DataTable dtupdt = condal.CON_Contact_SelectByPK(strcon,ContactID ,UserID);


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
            CON_DAL condal = new CON_DAL();
            DataTable dt = condal.CON_Contact_SelectAll(strcon, UserID);

            /*To pass country drop down for filter in Contact list */
            SqlConnection conn = new SqlConnection(strcon);
            LOC_DAL locdal = new LOC_DAL();
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
            return View("ContactList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            CON_DAL condal = new CON_DAL();
            condal.CON_Contact_DeleteByPK(str,ContactID, UserID);
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
                CON_DAL condal = new CON_DAL();
                String strmessage = condal.CON_Contact_Insert(str, UserID,modelLOC_Contact);
            }
            else
            {
                CON_DAL condal = new CON_DAL();
                String strmessage = condal.CON_Contact_Update(str, UserID, modelLOC_Contact);
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
            LOC_DAL locdal = new LOC_DAL();
            DataTable dt = locdal.dbo_PR_LOC_State_SelectDropDownByCountryID(str, CountryID, UserID);
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
            LOC_DAL locdal = new LOC_DAL();
            DataTable dt=locdal.LOC_City_SelectDropDownByStateID(str,StateID,UserID);
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
            CON_DAL condal = new CON_DAL();
            DataTable dt = condal.Contact_Filter(str,CountryID,StateID,CityID,ContactName,UserID);


            /*To pass country drop down for filter in Contact list */
            LOC_DAL locdal = new LOC_DAL();
            DataTable dt1=locdal.LOC_Country_SelectForDropDown(str, UserID);
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