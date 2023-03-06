using KevalThemeAddressBook.Areas.MST_ContactCategory.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.DAL
{
    public class MST_DALBASE:DALHelper
    {
        int UserID = 1;
        #region ContactCategory_SelectAll
        public DataTable ContactCategory_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region ContactCategory_SelectAll
        public DataTable ContactCategory_DropDownList()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_SelectForDropDownList");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region ContactCategory_SelectByPK
        public DataTable ContactCategory_SelectByPK(int? CategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_SelectByPK");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, CategoryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region ContactCategory_Insert
        public void ContactCategory_Insert(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_Insert");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.NVarChar, modelMST_ContactCategory.ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.Date, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.Date, DBNull.Value);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region ContactCategory_Update
        public void ContactCategory_Update(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.NVarChar, modelMST_ContactCategory.ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, modelMST_ContactCategory.ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.Date, DBNull.Value);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region ContactCategory_DeleteByPK
        public void ContactCategory_DeleteByPK(int CategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, CategoryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex) { }
        }
        #endregion
    }
}
