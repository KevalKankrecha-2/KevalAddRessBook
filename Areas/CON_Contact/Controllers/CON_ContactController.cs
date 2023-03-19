using Microsoft.AspNetCore.Mvc;
using KevalThemeAddressBook.Areas.LOC_Country.Models;
using KevalThemeAddressBook.Areas.LOC_State.Models;
using KevalThemeAddressBook.Areas.LOC_City.Models;
using KevalThemeAddressBook.Areas.CON_Contact.Models;
using KevalThemeAddressBook.Areas.MST_ContactCategory.Models;
using System.Data;
using System.Collections.Generic;
using System;
using System.IO;
using KevalThemeAddressBook.DAL;
using KevalThemeAddressBook.BAL;

namespace KevalThemeAddressBook.Areas.CON_Contact.Controllers
{
    [CheckAccess]
    [Area("CON_Contact")]
    public class CON_ContactController : Controller
    {
        
        #region Open Contact Form
        public IActionResult Add(int? ContactID)
        {
            #region Get Country Drop Down And Pass it Where Form Open in Add/Edit Mode

            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dtCountryDropDownList = dalLOC.LOC_Country_SelectForDropDownListByUserID();
            List<LOC_CountryDropDownModel> CountryDropDownList = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dtCountryDropDownList.Rows)
            {
                LOC_CountryDropDownModel CountryDropDown = new LOC_CountryDropDownModel();
                CountryDropDown.CountryID = (int)dr["CountryID"];
                CountryDropDown.CountryName = (string)dr["CountryName"];
                CountryDropDownList.Add(CountryDropDown);
            }
            ViewBag.CountryList = CountryDropDownList;
            #endregion

            #region Get Contact Category Drop Down And Pass it Where Form Open in Add/Edit Mode
            MST_DAL dalMST = new MST_DAL();
            DataTable dtContactCategoryDropDownList = dalMST.ContactCategory_DropDownListByUserID();
            List<ContactCategoryDropDown> ContactCategoryDropDownList = new List<ContactCategoryDropDown>();
            foreach (DataRow dr in dtContactCategoryDropDownList.Rows)
            {
                ContactCategoryDropDown ContactCategoryDropDown = new ContactCategoryDropDown();
                ContactCategoryDropDown.ContactCaregoryID = (int)dr["ContactCategoryID"];
                ContactCategoryDropDown.ContactCategoryName = (string)dr["ContactCategoryName"];
                ContactCategoryDropDownList.Add(ContactCategoryDropDown);
            }
            ViewBag.ContactCategoryDropDownList = ContactCategoryDropDownList;
            #endregion

            #region Contact Select By PK
            CON_ContactModel modelCON_Contact = new CON_ContactModel();
            if (ContactID != null)
            {
                CON_DAL dalCON = new CON_DAL();
                DataTable dtContact = dalCON.CON_Contact_SelectByPKUserID(ContactID);
               
                foreach (DataRow drupt in dtContact.Rows)
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
                    modelCON_Contact.BirthDate = Convert.ToDateTime(drupt["BirthDate"]);
                    modelCON_Contact.BloodGroup = Convert.ToString(drupt["BloodGroup"]);
                    ViewBag.EditImageURL= Convert.ToString(drupt["PhotoPath"]);
                    modelCON_Contact.PhotoPath= Convert.ToString(drupt["PhotoPath"]);
                }

                #region Get State From Country
                DataTable dtStateDropDownList = dalLOC.LOC_State_SelectDropDownByCountryIDUserID(modelCON_Contact.CountryID);
                List<LOC_StateDropDown> StateDropDownList = new List<LOC_StateDropDown>();
                foreach (DataRow dr in dtStateDropDownList.Rows)
                {
                    LOC_StateDropDown StateDropDown = new LOC_StateDropDown();
                    StateDropDown.StateID = Convert.ToInt32(dr["StateID"]);
                    StateDropDown.StateName = Convert.ToString(dr["StateName"]);
                    StateDropDownList.Add(StateDropDown);
                }
                ViewBag.StateList = StateDropDownList;
                #endregion 

                # region Get City From State
                DataTable dtCityDropDownList = dalLOC.LOC_City_SelectDropDownByStateIDUserID(modelCON_Contact.StateID);
                List<LOC_CityDropDown> CityDropDownList = new List<LOC_CityDropDown>();
                foreach (DataRow dr in dtCityDropDownList.Rows)
                {
                    LOC_CityDropDown CityDropDown = new LOC_CityDropDown();
                    CityDropDown.CityID = Convert.ToInt32(dr["CityID"]);
                    CityDropDown.CityName = Convert.ToString(dr["CityName"]);
                    CityDropDownList.Add(CityDropDown);
                }
                ViewBag.CityList = CityDropDownList;
                #endregion 
            }

            #region Pass State And City Empty
            else
            {
                List<LOC_StateDropDown> StateDropDown = new List<LOC_StateDropDown>();
                ViewBag.StateList = StateDropDown;
                List<LOC_CityDropDown> CityDropDown = new List<LOC_CityDropDown>();
                ViewBag.CityList = CityDropDown;
            }
            #endregion

            return View("CON_ContactAddEdit", modelCON_Contact);
        }
        #endregion

        #endregion

        #region Load Contacts
        public IActionResult Index()
        {
            CON_DAL dalCON = new CON_DAL();
            List<LOC_CountryDropDownModel> CountryDropDownList = new List<LOC_CountryDropDownModel>();
            DataTable dtContacList = dalCON.CON_Contact_SelectByUserID();

            /*To pass country drop down for filter in Contact list */
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dtCountryDropDownList = dalLOC.LOC_Country_SelectForDropDownListByUserID();
            foreach (DataRow dr1 in dtCountryDropDownList.Rows)
            {
                LOC_CountryDropDownModel CountryDropDown = new LOC_CountryDropDownModel();
                CountryDropDown.CountryID = (int)dr1["CountryID"];
                CountryDropDown.CountryName = (string)dr1["CountryName"];
                CountryDropDownList.Add(CountryDropDown);
            }
            ViewBag.CountryList = CountryDropDownList;
            /*end*/
            return View("CON_ContactList", dtContacList);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {
            CON_DAL dalCON = new CON_DAL();
            dalCON.CON_Contact_DeleteByPKUserID(ContactID);
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

            CON_DAL dalCON = new CON_DAL();
            if (modelLOC_Contact.ContactID == null)
            {
                String strmessage = dalCON.CON_Contact_InsertByUserID(modelLOC_Contact);
                TempData["ContactMsg"] = "Contact Inserted successfully.!";
            }
            else
            {
                String strmessage = dalCON.CON_Contact_UpdateByPKUserID(modelLOC_Contact);
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
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.LOC_State_SelectDropDownByCountryIDUserID(CountryID);
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

        public IActionResult DropdownByStateID(int StateID)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt=dalLOC.LOC_City_SelectDropDownByStateIDUserID(StateID);
            List<LOC_CityDropDown> CityDropDownList = new List<LOC_CityDropDown>();
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

            CON_DAL dalCON = new CON_DAL();
            DataTable dtContactFilterData = dalCON.Contact_Filter(CountryID,StateID,CityID,ContactName);


            /*To pass country drop down for filter in Contact list */
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt1=dalLOC.LOC_Country_SelectForDropDownListByUserID();
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

            return View("CON_ContactList", dtContactFilterData);
        }
        #endregion
    }
}