using KevalThemeAddressBook.BAL;
using KevalThemeAddressBook.Areas.CON_Contact.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.DAL
{
    public class CON_DALBASE:DALHelper
    {
        int UserID = (int)CommonVariables.UserID();
        #region CON_Contact_SelectByUserID
        public DataTable CON_Contact_SelectByUserID()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_CON_Contact_SelectByUserID");
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

        #region CON_Contact_SelectByPKUserID
        public DataTable CON_Contact_SelectByPKUserID(int? ContactID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_CON_Contact_SelectByPKUserID");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, ContactID);
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

        #region CON_Contact_InsertByUserID
        public string CON_Contact_InsertByUserID(CON_ContactModel modelCON_Contact)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_CON_Contact_InsertByUserID");
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

        #region CON_Contact_UpdateByPKUserID
        public string CON_Contact_UpdateByPKUserID(CON_ContactModel modelCON_Contact)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_CON_Contact_UpdateByPKUserID");
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

        #region CON_Contact_DeleteByPKUserID
        public void CON_Contact_DeleteByPKUserID(int ContactID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_CON_Contact_DeleteByPKUserID");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, ContactID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex) { }
        }
        #endregion
    }
}
