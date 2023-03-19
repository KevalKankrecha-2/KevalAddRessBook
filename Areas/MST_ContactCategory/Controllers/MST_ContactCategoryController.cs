using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using KevalThemeAddressBook.Models;
using System;
using KevalThemeAddressBook.DAL;
using KevalThemeAddressBook.Areas.MST_ContactCategory.Models;
using KevalThemeAddressBook.BAL;

namespace KevalThemeAddressBook.Areas.MST_ContactCategory.Controllers
{
    [CheckAccess]
    [Area("MST_ContactCategory")]
    public class MST_ContactCategoryController : Controller
    {
        #region Open Contact Category Form
        public IActionResult Add(int? ContactCategoryID)
        {
            MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();
            MST_DAL dalMST = new MST_DAL();
            if (ContactCategoryID != null)
            {
                DataTable dtContactCategory = dalMST.ContactCategory_SelectByPKUserID(ContactCategoryID);
                foreach (DataRow dr in dtContactCategory.Rows)
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
            MST_DAL dalMST = new MST_DAL();
            DataTable dtContactCategoryList = dalMST.MST_ContactCategory_SelectByUserID();

            return View("MST_ContactCategoryList", dtContactCategoryList);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            MST_DAL dalMST = new MST_DAL();
            dalMST.MST_ContactCategory_DeleteByPKUserID(ContactCategoryID);
            TempData["ContactCatMsg"] = "Contact Category Deleted successfully.!";
            return RedirectToAction("Index");
        }
        #endregion

        #region Add Edit Contact Catrgory
        [HttpPost]
        public IActionResult Save(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            MST_DAL dalMST = new MST_DAL();
            if (modelMST_ContactCategory.ContactCategoryID == null)
            {
                dalMST.MST_ContactCategory_InsertByUserID(modelMST_ContactCategory);
                TempData["ContactCatMsg"] = "Contact Category Inserted successfully.!";
            }
            else
            {
                dalMST.MST_ContactCategory_UpdateByPKUserID(modelMST_ContactCategory);
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
