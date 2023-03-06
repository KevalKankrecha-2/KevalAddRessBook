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
        MST_DAL dalMST = new MST_DAL();

        #region Open Contact Category Form
        public IActionResult OpenPage(int? ContactCategoryID)
        {
            MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();
            if (ContactCategoryID != null)
            {
                string strcon = this.Configuration.GetConnectionString("myConnectionString");
                DataTable dtupt = dalMST.ContactCategory_SelectByPK(strcon, ContactCategoryID, UserID);
                foreach (DataRow dr in dtupt.Rows)
                {
                    modelMST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelMST_ContactCategory.ContactCategoryName = Convert.ToString(dr["ContactCategoryName"]);
                }
            }
            return View("MST_ContactCategoryAddEdit", modelMST_ContactCategory);
        }
        #endregion

        #region Contact Category List
        public IActionResult Index()
        {
            string strcon = this.Configuration.GetConnectionString("myConnectionString");
            DataTable dt = dalMST.ContactCategory_SelectAll(strcon, UserID);
            return View("MST_ContactCategoryList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            dalMST.ContactCategory_DeleteByPK(str, ContactCategoryID, UserID);
            TempData["ContactCatMsg"] = "Contact Category Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion

        #region Add Edit Contact Catrgory
        [HttpPost]
        public IActionResult Save(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            if (modelMST_ContactCategory.ContactCategoryID == null)
            {
                dalMST.ContactCategory_Insert(str, modelMST_ContactCategory, UserID);
                TempData["ContactCatMsg"] = "Contact Category Inserted successfully.!";
            }
            else
            {
                dalMST.ContactCategory_Update(str, modelMST_ContactCategory, UserID);
                TempData["ContactCatMsg"] = "Contact Category Updated successfully.!";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Cancel
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
        #endregion
    }
}
