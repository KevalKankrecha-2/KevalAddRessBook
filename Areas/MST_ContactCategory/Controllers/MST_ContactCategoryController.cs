using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using KevalThemeAddressBook.Models;
using System;
using KevalThemeAddressBook.DAL;
using KevalThemeAddressBook.Areas.MST_ContactCategory.Models;

namespace KevalThemeAddressBook.Areas.MST_ContactCategory.Controllers
{
    [Area("MST_ContactCategory")]
    public class MST_ContactCategoryController : Controller
    {
        private IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        int UserID = 1;
        #region Open Contact Category Form
        public IActionResult OpenPage(int? ContactCategoryID)
        {
            MST_ContactCategoryModel modelcontactcategory = new MST_ContactCategoryModel();
            if (ContactCategoryID != null)
            {
                string strcon = this.Configuration.GetConnectionString("myConnectionString");
                MST_DALBASE contactcatdal = new MST_DALBASE();
                DataTable dtupt = contactcatdal.ContactCategory_SelectByPK(strcon,ContactCategoryID,UserID);
                foreach (DataRow dr in dtupt.Rows)
                {
                    modelcontactcategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelcontactcategory.ContactCategoryName = Convert.ToString(dr["ContactCategoryName"]);
                }
            }
            return View("MST_ContactCategoryAddEdit", modelcontactcategory);
        }
        #endregion

        #region Contact Category List
        public IActionResult Index()
        {
            string strcon = this.Configuration.GetConnectionString("myConnectionString");
            MST_DALBASE contactcatdal = new MST_DALBASE();
            DataTable dt = contactcatdal.ContactCategory_SelectAll(strcon, UserID);
            return View("MST_ContactCategoryList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            MST_DALBASE contactcatdal = new MST_DALBASE();
            contactcatdal.ContactCategory_DeleteByPK(str,ContactCategoryID,UserID);
            TempData["ContactCatMsg"] = "Contact Category Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion

        #region Add Edit Contact Catrgory
        [HttpPost]
        public IActionResult Save(MST_ContactCategoryModel modelContactCategory)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            if (modelContactCategory.ContactCategoryID == null)
            {
                MST_DALBASE contactcatdal = new MST_DALBASE();
                contactcatdal.ContactCategory_Insert(str,modelContactCategory,UserID);
                TempData["ContactCatMsg"] = "Contact Category Inserted successfully.!";
            }
            else
            {
                MST_DALBASE contactcatdal = new MST_DALBASE();
                contactcatdal.ContactCategory_Update(str, modelContactCategory, UserID);
                TempData["ContactCatMsg"] = "Contact Category Updated successfully.!";
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
