using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using KevalThemeAddressBook.Models;
using System;
using KevalThemeAddressBook.DAL;

namespace KevalAddressBook.Controllers
{
    public class ContactCategoryController : Controller
    {
        private IConfiguration Configuration;
        public ContactCategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        int UserID = 1;
        #region Open Contact Category Form
        public IActionResult OpenPage(int? ContactCategoryID)
        {
            ContactCategoryModel modelcontactcategory = new ContactCategoryModel();
            if (ContactCategoryID != null)
            {
                string strcon = this.Configuration.GetConnectionString("myConnectionString");
                Contact_Category_DAL contactcatdal = new Contact_Category_DAL();
                DataTable dtupt = contactcatdal.ContactCategory_SelectByPK(strcon,ContactCategoryID,UserID);
                foreach (DataRow dr in dtupt.Rows)
                {
                    modelcontactcategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelcontactcategory.ContactCategoryName = Convert.ToString(dr["ContactCategoryName"]);
                }
            }
            return View("ContactCategoryAddEdit", modelcontactcategory);
        }
        #endregion

        #region Contact Category List
        public IActionResult Index()
        {
            string strcon = this.Configuration.GetConnectionString("myConnectionString");
            Contact_Category_DAL contactcatdal = new Contact_Category_DAL();
            DataTable dt = contactcatdal.ContactCategory_SelectAll(strcon, UserID);
            return View("ContactCategoryList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            Contact_Category_DAL contactcatdal = new Contact_Category_DAL();
            contactcatdal.ContactCategory_DeleteByPK(str,ContactCategoryID,UserID);
            return RedirectToAction("Index");
        }
        #endregion

        #region Add Edit Contact Catrgory
        [HttpPost]
        public IActionResult Save(ContactCategoryModel modelContactCategory)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            if (modelContactCategory.ContactCategoryID == null)
            {
                Contact_Category_DAL contactcatdal = new Contact_Category_DAL();
                contactcatdal.ContactCategory_Insert(str,modelContactCategory,UserID);
            }
            else
            {
                Contact_Category_DAL contactcatdal = new Contact_Category_DAL();
                contactcatdal.ContactCategory_Update(str, modelContactCategory, UserID);
            }
            return RedirectToAction("Index");
        }
        #endregion

        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
    }
}
