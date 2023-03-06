using Microsoft.AspNetCore.Mvc;
using KevalThemeAddressBook.Areas.LOC_Country.Models;
using KevalThemeAddressBook.Areas.LOC_State.Models;
using KevalThemeAddressBook.Areas.LOC_City.Models;
using KevalThemeAddressBook.Areas.CON_Contact.Models;
using KevalThemeAddressBook.Areas.MST_ContactCategory.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.IO;
using KevalThemeAddressBook.DAL;

namespace KevalThemeAddressBook.Areas.CON_Contact.Controllers
{
    [Area("CON_Contact")]
    public class CON_ContactController : Controller
    {
        private IConfiguration Configuration;
        public CON_ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        int UserID = 1;
        CON_ContactModel modelCON_Contact = new CON_ContactModel();
        List<LOC_CountryDropDownModel> CountryDropDownList = new List<LOC_CountryDropDownModel>();
        List<LOC_StateDropDown> StateDropDownList = new List<LOC_StateDropDown>();
        List<LOC_CityDropDown> CityDropDownList = new List<LOC_CityDropDown>();
        List<ContactCategoryDropDown> ContactCategoryDropDownList = new List<ContactCategoryDropDown>();
        CON_DAL dalCON = new CON_DAL();
        LOC_DAL dalLOC = new LOC_DAL();
        MST_DAL dalMST = new MST_DAL();
        #region Open Contact Form
        public IActionResult OpenPage(int? ContactID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");

            #region Get Country Drop Down And Pass it Where Form Open in Add/Edit Mode
           
            DataTable dt= dalLOC.LOC_Country_SelectForDropDown(str, UserID);
            foreach (DataRow dr in dt.Rows)
            {
                LOC_CountryDropDownModel CountryDropDown = new LOC_CountryDropDownModel();
                CountryDropDown.CountryID = (int)dr["CountryID"];
                CountryDropDown.CountryName = (string)dr["CountryName"];
                CountryDropDownList.Add(CountryDropDown);
            }
            ViewBag.CountryList = CountryDropDownList;
            #endregion

            #region Get Contact Category Drop Down And Pass it Where Form Open in Add/Edit Mode
            DataTable dtccddd = dalMST.ContactCategory_DropDownList(str, UserID);
            foreach (DataRow dr in dtccddd.Rows)
            {
                ContactCategoryDropDown ContactCategoryDropDown = new ContactCategoryDropDown();
                ContactCategoryDropDown.ContactCaregoryID = (int)dr["ContactCategoryID"];
                ContactCategoryDropDown.ContactCategoryName = (string)dr["ContactCategoryName"];
                ContactCategoryDropDownList.Add(ContactCategoryDropDown);
            }
            ViewBag.ContactCategoryDropDownList = ContactCategoryDropDownList;
            #endregion

            #region Contact Select By PK
            if (ContactID != null)
            {
                string strcon = this.Configuration.GetConnectionString("myConnectionString");
              
                DataTable dtupdt = dalCON.CON_Contact_SelectByPK(strcon,ContactID ,UserID);
                foreach (DataRow drupt in dtupdt.Rows)
                {
                    modelCON_Contact.ContactID = Convert.ToInt32(drupt["ContactID"]);
                    modelCON_Contact.ContactName = Convert.ToString(drupt["ContactName"]);
                    modelCON_Contact.ContactCategoryID = Convert.ToInt32(drupt["ContectCategoryID"]);
                    modelCON_Contact.ContactNo = Convert.ToString(drupt["ContactNo"]);
                    modelCON_Contact.WhatsappNo = Convert.ToString(drupt["WhatsappNo"]);
                    modelCON_Contact.Email = Convert.ToString(drupt["Email"]);
                    modelCON_Contact.Address = Convert.ToString(drupt["Address"]);
                    modelCON_Contact.FaceBookID = Convert.ToString(drupt["FaceBookID"]);
                    modelCON_Contact.InstaID = Convert.ToString(drupt["InstaID"]);
                    modelCON_Contact.CountryID = Convert.ToInt32(drupt["CountryID"]);
                    modelCON_Contact.StateID = Convert.ToInt32(drupt["StateID"]);
                    modelCON_Contact.CityID = Convert.ToInt32(drupt["CityID"]);
                    modelCON_Contact.Age = Convert.ToInt32(drupt["Age"]);
                    modelCON_Contact.BirthDate = Convert.ToDateTime(drupt["BirthDate"]);
                    modelCON_Contact.BloodGroup = Convert.ToString(drupt["BloodGroup"]);
                    ViewBag.EditImageURL= Convert.ToString(drupt["PhotoPath"]);
                    modelCON_Contact.PhotoPath= Convert.ToString(drupt["PhotoPath"]);
                }

                #region Get State From Country
                dt = dalLOC.dbo_PR_LOC_State_SelectDropDownByCountryID(strcon, modelCON_Contact.CountryID, UserID);
                foreach (DataRow dr in dt.Rows)
                {
                    LOC_StateDropDown StateDropDown = new LOC_StateDropDown();
                    StateDropDown.StateID = Convert.ToInt32(dr["StateID"]);
                    StateDropDown.StateName = Convert.ToString(dr["StateName"]);
                    StateDropDownList.Add(StateDropDown);
                }
                ViewBag.StateList = StateDropDownList;
                #endregion 

                # region Get City From State
                dt = dalLOC.LOC_City_SelectDropDownByStateID(strcon, modelCON_Contact.StateID, UserID);
                foreach (DataRow dr in dt.Rows)
                {
                    LOC_CityDropDown CityDropDown = new LOC_CityDropDown();
                    CityDropDown.CityID = Convert.ToInt32(dr["CityID"]);
                    CityDropDown.CityName = Convert.ToString(dr["CityName"]);
                    CityDropDownList.Add(CityDropDown);
                }
                ViewBag.CityList = CityDropDownList;
                #endregion 
            }
            else
            {
                List<LOC_StateDropDown> StateDropDown = new List<LOC_StateDropDown>();
                ViewBag.StateList = StateDropDown;
                List<LOC_CityDropDown> CityDropDown = new List<LOC_CityDropDown>();
                ViewBag.CityList = CityDropDown;
            }

            return View("CON_ContactAddEdit", modelCON_Contact);
        }
        #endregion

        #endregion

        #region Load Contacts
        public IActionResult Index()
        {
            string strcon = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalCON.CON_Contact_SelectAll(strcon, UserID);

            /*To pass country drop down for filter in Contact list */
            SqlConnection conn = new SqlConnection(strcon);
            LOC_DAL dalLOC = new LOC_DAL();
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
            return View("CON_ContactList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            dalCON.CON_Contact_DeleteByPK(str,ContactID, UserID);
            TempData["ContactMsg"] = "Contact Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion

        #region Add Edit Contacts
        public IActionResult Save(CON_ContactModel modelLOC_Contact)
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
                String strmessage = dalCON.CON_Contact_Insert(str, UserID,modelLOC_Contact);
                TempData["ContactMsg"] = "Contact Inserted successfully.!";
            }
            else
            {
                String strmessage = dalCON.CON_Contact_Update(str, UserID, modelLOC_Contact);
                TempData["ContactMsg"] = "Contact Updated successfully.!";
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
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.dbo_PR_LOC_State_SelectDropDownByCountryID(str, CountryID, UserID);
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

        public IActionResult DropdownByStateID(int StateID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt=dalLOC.LOC_City_SelectDropDownByStateID(str,StateID,UserID);
            foreach (DataRow dr in dt.Rows)
            {
                LOC_CityDropDown dropdown = new LOC_CityDropDown();
                dropdown.CityID = Convert.ToInt32(dr["CityID"]);
                dropdown.CityName = dr["CityName"].ToString();
                CityDropDownList.Add(dropdown);
            }
            var vModel = CityDropDownList;
            return Json(vModel);
        }
        #endregion

        #region ContactFilter
        public IActionResult Contact_Filter(int CountryID,int StateID,int CityID,string ContactName)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalCON.Contact_Filter(str,CountryID,StateID,CityID,ContactName,UserID);


            /*To pass country drop down for filter in Contact list */
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt1=dalLOC.LOC_Country_SelectForDropDown(str, UserID);
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel CountryDropDown = new LOC_CountryDropDownModel();
                CountryDropDown.CountryID = (int)dr1["CountryID"];
                CountryDropDown.CountryName = (string)dr1["CountryName"];
                CountryDropDownList.Add(CountryDropDown);
            }
            ViewBag.CountryList = CountryDropDownList;
            /*end*/

            return View("CON_ContactList",dt);
        }
        #endregion
    }
}