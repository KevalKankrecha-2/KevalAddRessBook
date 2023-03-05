using KevalThemeAddressBook.Models;
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
    public class MST_DAL
    {
        #region ContactCategory_SelectAll
        public DataTable ContactCategory_SelectAll(string conn,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_MST_ContactCategory_SelectAll");
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDb.ExecuteReader(dbCmd))
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
        public DataTable ContactCategory_DropDownList(string conn, int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_MST_ContactCategory_SelectForDropDownList");
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDb.ExecuteReader(dbCmd))
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
        public DataTable ContactCategory_SelectByPK(string conn, int? CategoryID,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_MST_ContactCategory_SelectByPK");
                sqlDb.AddInParameter(dbCmd, "ContactCategoryID", SqlDbType.Int, CategoryID);
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDb.ExecuteReader(dbCmd))
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
        public void ContactCategory_Insert(string conn, MST_ContactCategoryModel modelMST_ContactCategory,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_MST_ContactCategory_Insert");
                sqlDb.AddInParameter(dbCmd, "ContactCategoryName", SqlDbType.NVarChar, modelMST_ContactCategory.ContactCategoryName);
                sqlDb.AddInParameter(dbCmd, "CreationTime", SqlDbType.Date, DBNull.Value);
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                sqlDb.AddInParameter(dbCmd, "ModificationTime", SqlDbType.Date, DBNull.Value);
                sqlDb.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region ContactCategory_Update
        public void ContactCategory_Update(string conn, MST_ContactCategoryModel modelMST_ContactCategory, int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_MST_ContactCategory_UpdateByPK");
                sqlDb.AddInParameter(dbCmd, "ContactCategoryName", SqlDbType.NVarChar, modelMST_ContactCategory.ContactCategoryName);
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                sqlDb.AddInParameter(dbCmd, "ContactCategoryID", SqlDbType.Int, modelMST_ContactCategory.ContactCategoryID);
                sqlDb.AddInParameter(dbCmd, "ModificationTime", SqlDbType.Date, DBNull.Value);
                sqlDb.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region ContactCategory_DeleteByPK
        public void ContactCategory_DeleteByPK(string conn, int CategoryID,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_MST_ContactCategory_DeleteByPK");
                sqlDb.AddInParameter(dbCmd, "ContactCategoryID", SqlDbType.Int, CategoryID);
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                sqlDb.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex) { }
        }
        #endregion
    }
}
