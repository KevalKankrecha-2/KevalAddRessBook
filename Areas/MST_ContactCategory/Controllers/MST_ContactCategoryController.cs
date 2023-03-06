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
        MST_DAL dalMST = new MST_DAL();

        #region Open Contact Category Form
        public IActionResult OpenPage(int? ContactCategoryID)
        {
            MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();
            if (ContactCategoryID != null)
            {
                DataTable dtupt = dalMST.ContactCategory_SelectByPK(ContactCategoryID);
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
            DataTable dt = dalMST.ContactCategory_SelectAll();
            return View("MST_ContactCategoryList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            dalMST.ContactCategory_DeleteByPK(ContactCategoryID);
            TempData["ContactCatMsg"] = "Contact Category Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion

        #region Add Edit Contact Catrgory
        [HttpPost]
        public IActionResult Save(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            if (modelMST_ContactCategory.ContactCategoryID == null)
            {
                dalMST.ContactCategory_Insert(modelMST_ContactCategory);
                TempData["ContactCatMsg"] = "Contact Category Inserted successfully.!";
            }
            else
            {
                dalMST.ContactCategory_Update(modelMST_ContactCategory);
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
