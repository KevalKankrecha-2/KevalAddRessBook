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
                SelectByPkDAL selectbypkdal = new SelectByPkDAL();
                DataTable dtupt = selectbypkdal.SelectByPk(strcon, UserID, "PR_ContactCategory_SelectByPK", "@ContactCategoryID", ContactCategoryID);

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
            DeleteSelectAll_DAL daldeleteselectall = new DeleteSelectAll_DAL();
            DataTable dt = daldeleteselectall.SelectAll(strcon, UserID, "PR_ContactCategory_SelectAll");
            return View("ContactCategoryList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            DeleteSelectAll_DAL daldeleteselectall = new DeleteSelectAll_DAL();
            daldeleteselectall.DeleteBYPK(str, UserID, "PR_ContactCategory_DeleteByPK","ContactCategoryID", ContactCategoryID);
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
                Insert_DAL insDAL = new Insert_DAL();
                String strmessage = insDAL.Insert_ContactCategory(str, UserID, "PR_ContactCategory_Insert", modelContactCategory.ContactCategoryName);
            }
            else
            {
                Update_DAL uptContactCategory = new Update_DAL();
                string strmessage = uptContactCategory.Update_ContactCategory(str, UserID, "PR_ContactCategory_UpdateByPK", modelContactCategory.ContactCategoryName,modelContactCategory.ContactCategoryID);
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
