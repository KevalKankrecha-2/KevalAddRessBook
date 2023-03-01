using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Data;
using System.Data.Common;

namespace KevalThemeAddressBook.DAL
{
    public class Insert_DAL
    {
        #region Insert_Country 
        public string Insert_Country(string conn, int UserID, string StoreProcedure,string CountryName,string CountryCode)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand(StoreProcedure);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.VarChar, CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.VarChar, CountryCode);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region Insert_State
        public string Insert_State(string conn, int UserID, string StoreProcedure, string StateName, string StateCode,int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand(StoreProcedure);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.VarChar, StateCode);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region Insert_City
        public string Insert_City(string conn, int UserID, string StoreProcedure, string CityName, string CityCode, int CountryID,int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand(StoreProcedure);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.VarChar, CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.VarChar, CityCode);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region Insert_ContactCategory
        public string Insert_ContactCategory(string conn, int UserID, string StoreProcedure,  string ContactCategoryName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand(StoreProcedure);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.VarChar, ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region Insert_Contact
        public string Insert_Contact(string conn, int UserID, string StoreProcedure,string ContactName,int CountryID,int StateID,int CityID,int ContactCategoryID,string ContactNo,string WhatsappNo,DateTime BirthDate,string Email,int Age,string Address,string BloodGroup,string FaceBookID,string InstaID,string PhotoPath)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand(StoreProcedure);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ContactName", SqlDbType.VarChar, ContactName);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "ContactNo", SqlDbType.VarChar, ContactNo);
                sqlDB.AddInParameter(dbCMD, "WhatsappNo", SqlDbType.VarChar  , WhatsappNo);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.DateTime, BirthDate);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, Email);
                sqlDB.AddInParameter(dbCMD, "Age", SqlDbType.Int, Age);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.VarChar, Address);
                sqlDB.AddInParameter(dbCMD, "BloodGroup", SqlDbType.VarChar, BloodGroup);
                sqlDB.AddInParameter(dbCMD, "FaceBookID", SqlDbType.VarChar, FaceBookID);
                sqlDB.AddInParameter(dbCMD, "InstaID", SqlDbType.VarChar, InstaID);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, PhotoPath);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion
    }
}



