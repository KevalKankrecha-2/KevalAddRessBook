using KevalThemeAddressBook.Areas.LOC_Country.Models;
using KevalThemeAddressBook.Areas.LOC_State.Models;
using KevalThemeAddressBook.Areas.LOC_City.Models;
using KevalThemeAddressBook.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using KevalThemeAddressBook.BAL;

namespace KevalThemeAddressBook.DAL
{
    public class LOC_DALBASE:DALHelper
    {

        int UserID = (int)CommonVariables.UserID();
        #region LOC_Country_SelectByUserID
        public DataTable LOC_Country_SelectByUserID()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_SelectByUserID");
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

        #region LOC_Country_SelectByPKUserID
        public DataTable LOC_Country_SelectByPKUserID(int? CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectByPKUserID");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
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

        #region LOC_Country_InsertByUserID
        public string LOC_Country_InsertByUserID(LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_InsertByUserID");
                    sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName.Trim());
                    sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.VarChar, modelLOC_Country.CountryCode.Trim());
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region LOC_Country_UpdateByPKUserID
        public string LOC_Country_UpdateByPKUserID(LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByPKUserID");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_Country.CountryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName.Trim());
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.VarChar, modelLOC_Country.CountryCode.Trim());
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.Date, DBNull.Value);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_CountryDeleteByPKUserID 
        public void LOC_CountryDeleteByPKUserID(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_DeleteByPKUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception e)
            {
            }
        }
        #endregion


        #region LOC_State_SelectByUserID
        public DataTable LOC_State_SelectByUserID()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectByUserID");
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

        #region LOC_State_SelectByPKUserID
        public DataTable LOC_State_SelectByPKUserID(int? StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectByPKUserID");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
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

        #region LOC_State_InsertByUserID
        public string LOC_State_InsertByUserID(LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_InsertByUserID");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_State.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, modelLOC_State.StateName.Trim());
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.VarChar, modelLOC_State.StateCode.Trim());
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "UserId", SqlDbType.Int, UserID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_State_UpdateByPKUserID
        public string LOC_State_UpdateByPKUserID(LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_UpdateByPKUserID");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_State.CountryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_State.StateID);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, modelLOC_State.StateName.Trim());
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.VarChar, modelLOC_State.StateCode.Trim());
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.Date, DBNull.Value);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_StateDeleteByPKUserID 
        public void LOC_StateDeleteByPKUserID(int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_DeleteByPKUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception e)
            {
            }
        }
        #endregion



        #region LOC_City_SelectByUserID
        public DataTable LOC_City_SelectByUserID()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectByUserID");
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

        #region LOC_City_SelectByPKUserID
        public DataTable LOC_City_SelectByPKUserID(int? CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectByPKUserID");
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
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

        #region LOC_City_InsertByUserID
        public string LOC_City_InsertByUserID(LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_InsertByUserID");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName.Trim());
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.VarChar, modelLOC_City.CityCode.Trim());
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);

                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_City_UpdateByPKUserID
        public void LOC_City_UpdateByPKUserID(LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_UpdateByPKUserID");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelLOC_City.CityID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName.Trim());
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.VarChar, modelLOC_City.CityCode.Trim());

                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.Date, DBNull.Value);

                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex) { }

        }
        #endregion

        #region LOC_CityDeleteBYPKUserID
        public void LOC_CityDeleteBYPKUserID(int CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_DeleteByPKUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception e)
            {
            }
        }
        #endregion

        

    }
}