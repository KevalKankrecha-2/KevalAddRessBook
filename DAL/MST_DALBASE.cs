using KevalThemeAddressBook.Areas.MST_ContactCategory.Models;
using KevalThemeAddressBook.BAL;
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
        int UserID = (int)CommonVariables.UserID();
        #region MST_ContactCategory_SelectByUserID
        public DataTable MST_ContactCategory_SelectByUserID()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_SelectByUserID");
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

        #region ContactCategory_SelectByPKUserID
        public DataTable ContactCategory_SelectByPKUserID(int? CategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_SelectByPKUserID");
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

        #region MST_ContactCategory_InsertByUserID
        public void MST_ContactCategory_InsertByUserID(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_InsertByUserID");
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

        #region MST_ContactCategory_UpdateByPKUserID
        public void MST_ContactCategory_UpdateByPKUserID(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_UpdateByPKUserID");
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

        #region MST_ContactCategory_DeleteByPKUserID
        public void MST_ContactCategory_DeleteByPKUserID(int CategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_MST_ContactCategory_DeleteByPKUserID");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, CategoryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex) { }
        }
        #endregion
    }
}
