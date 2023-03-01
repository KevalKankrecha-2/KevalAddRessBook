using KevalThemeAddressBook.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.DAL
{
    public class CON_DALBASE
    {
        #region CON_Contact_SelectAll
        public DataTable CON_Contact_SelectAll(string conn,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_Contact_SelectAllRecord");
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

        #region CON_Contact_SelectByPK
        public DataTable CON_Contact_SelectByPK(string conn, int? ContactID,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_Contact_SelectByPK");
                sqlDb.AddInParameter(dbCmd, "ContactID", SqlDbType.Int, ContactID);
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

        #region CON_Contact_Insert
        public string CON_Contact_Insert(string conn,int UserID, LOC_ContactModel modelCON_Contact)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Contact_Insert");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ContactName", SqlDbType.VarChar, modelCON_Contact.ContactName);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelCON_Contact.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelCON_Contact.StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelCON_Contact.CityID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, modelCON_Contact.ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "ContactNo", SqlDbType.VarChar, modelCON_Contact.ContactNo);
                sqlDB.AddInParameter(dbCMD, "WhatsappNo", SqlDbType.VarChar, modelCON_Contact.WhatsappNo);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.DateTime, modelCON_Contact.BirthDate);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, modelCON_Contact.Email);
                sqlDB.AddInParameter(dbCMD, "Age", SqlDbType.Int, modelCON_Contact.Age);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.VarChar, modelCON_Contact.Address);
                sqlDB.AddInParameter(dbCMD, "BloodGroup", SqlDbType.VarChar, modelCON_Contact.BloodGroup);
                sqlDB.AddInParameter(dbCMD, "FaceBookID", SqlDbType.VarChar, modelCON_Contact.FaceBookID);
                sqlDB.AddInParameter(dbCMD, "InstaID", SqlDbType.VarChar, modelCON_Contact.InstaID);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, modelCON_Contact.PhotoPath);

                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region CON_Contact_Update
        public string CON_Contact_Update(string conn, int UserID, LOC_ContactModel modelCON_Contact)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_Contact_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, modelCON_Contact.ContactID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ContactName", SqlDbType.VarChar, modelCON_Contact.ContactName);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelCON_Contact.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelCON_Contact.StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelCON_Contact.CityID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, modelCON_Contact.ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "ContactNo", SqlDbType.VarChar, modelCON_Contact.ContactNo);
                sqlDB.AddInParameter(dbCMD, "WhatsappNo", SqlDbType.VarChar, modelCON_Contact.WhatsappNo);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.DateTime, modelCON_Contact.BirthDate);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, modelCON_Contact.Email);
                sqlDB.AddInParameter(dbCMD, "Age", SqlDbType.Int, modelCON_Contact.Age);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.VarChar, modelCON_Contact.Address);
                sqlDB.AddInParameter(dbCMD, "BloodGroup", SqlDbType.VarChar, modelCON_Contact.BloodGroup);
                sqlDB.AddInParameter(dbCMD, "FaceBookID", SqlDbType.VarChar, modelCON_Contact.FaceBookID);
                sqlDB.AddInParameter(dbCMD, "InstaID", SqlDbType.VarChar, modelCON_Contact.InstaID);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, modelCON_Contact.PhotoPath);

                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region CON_Contact_DeleteByPK
        public void CON_Contact_DeleteByPK(string conn, int ContactID,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_Contact_Delete");
                sqlDb.AddInParameter(dbCmd, "ContactID", SqlDbType.Int, ContactID);
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                sqlDb.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex) { }
        }
        #endregion
    }
}
