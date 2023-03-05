using KevalThemeAddressBook.Areas.LOC_Country.Models;
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
    public class LOC_DALBASE
    {

        #region LOC_Country_SelectAll
        public DataTable LOC_Country_SelectAll(string conn, int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_LOC_Country_SelectAll");
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

        #region LOC_State_SelectAll
        public DataTable LOC_State_SelectAll(string conn, int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_LOC_State_SelectAll");
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

        #region LOC_City_SelectAll
        public DataTable LOC_City_SelectAll(string conn, int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_LOC_City_SelectAll");
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

        #region LOC_Country_SelectByPK
        public DataTable LOC_Country_SelectByPK(string conn, int? CountryID, int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_LOC_Country_SelectByPK");
                sqlDb.AddInParameter(dbCmd, "CountryID", SqlDbType.Int, CountryID);
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

        #region LOC_State_SelectByPK
        public DataTable LOC_State_SelectByPK(string conn, int? StateID, int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_Loc_State_SelectBYPK");
                sqlDb.AddInParameter(dbCmd, "StateID", SqlDbType.Int, StateID);
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

        #region LOC_City_SelectByPK
        public DataTable LOC_City_SelectByPK(string conn, int? CityID, int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_LOC_City_SelectByPK");
                sqlDb.AddInParameter(dbCmd, "CityID", SqlDbType.Int, CityID);
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

        #region LOC_Country_Insert
        public string LOC_Country_Insert(string conn, int UserID, LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_LOC_Country_Insert");
                sqlDb.AddInParameter(dbCmd, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDb.AddInParameter(dbCmd, "CountryCode", SqlDbType.VarChar, modelLOC_Country.CountryCode);
                sqlDb.AddInParameter(dbCmd, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDb.AddInParameter(dbCmd, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                sqlDb.ExecuteNonQuery(dbCmd);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region LOC_State_Insert
        public string LOC_State_Insert(string conn, int UserID, LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_LOC_State_Insert");
                sqlDb.AddInParameter(dbCmd, "CountryID", SqlDbType.Int, modelLOC_State.CountryID);
                sqlDb.AddInParameter(dbCmd, "StateName", SqlDbType.VarChar, modelLOC_State.StateName);
                sqlDb.AddInParameter(dbCmd, "StateCode", SqlDbType.VarChar, modelLOC_State.StateCode);
                sqlDb.AddInParameter(dbCmd, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDb.AddInParameter(dbCmd, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDb.AddInParameter(dbCmd, "UserId", SqlDbType.Int, UserID);
                sqlDb.ExecuteNonQuery(dbCmd);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_City_Insert
        public string LOC_City_Insert(string conn, int USerID, LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_LOC_City_Insert");
                sqlDb.AddInParameter(dbCmd, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                sqlDb.AddInParameter(dbCmd, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                sqlDb.AddInParameter(dbCmd, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDb.AddInParameter(dbCmd, "CityCode", SqlDbType.VarChar, modelLOC_City.CityCode);
                sqlDb.AddInParameter(dbCmd, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDb.AddInParameter(dbCmd, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, USerID);

                sqlDb.ExecuteNonQuery(dbCmd);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_Country_UpdateByPK
        public string LOC_Country_UpdateByPK(string conn,int UserID, LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_LOC_Country_UpdateByPK");
                sqlDb.AddInParameter(dbCmd, "CountryID", SqlDbType.Int, modelLOC_Country.CountryID);
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                sqlDb.AddInParameter(dbCmd, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDb.AddInParameter(dbCmd, "CountryCode", SqlDbType.VarChar, modelLOC_Country.CountryCode);
                sqlDb.AddInParameter(dbCmd, "ModificationTime", SqlDbType.Date, DBNull.Value);
                sqlDb.ExecuteNonQuery(dbCmd);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_State_UpdateByPK
        public string LOC_State_UpdateByPK(string conn,int UserID, LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_LOC_State_UpdateByPK");
                sqlDb.AddInParameter(dbCmd, "CountryID", SqlDbType.Int, modelLOC_State.CountryID);
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                sqlDb.AddInParameter(dbCmd, "StateID", SqlDbType.Int, modelLOC_State.StateID);
                sqlDb.AddInParameter(dbCmd, "StateName", SqlDbType.NVarChar, modelLOC_State.StateName);
                sqlDb.AddInParameter(dbCmd, "StateCode", SqlDbType.VarChar, modelLOC_State.StateCode);
                sqlDb.AddInParameter(dbCmd, "ModificationTime", SqlDbType.Date, DBNull.Value);
                sqlDb.ExecuteNonQuery(dbCmd);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_City_UpdateByPK
        public void LOC_City_UpdateByPK(string conn,int UserID, LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_LOC_City_UpdateByPK");
                sqlDb.AddInParameter(dbCmd, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                sqlDb.AddInParameter(dbCmd, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                sqlDb.AddInParameter(dbCmd, "CityID", SqlDbType.Int, modelLOC_City.CityID);
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                sqlDb.AddInParameter(dbCmd, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDb.AddInParameter(dbCmd, "CityCode", SqlDbType.VarChar, modelLOC_City.CityCode);

                sqlDb.AddInParameter(dbCmd, "ModificationTime", SqlDbType.Date, DBNull.Value);

                sqlDb.ExecuteNonQuery(dbCmd);
            }
            catch (Exception ex) { }

        }
        #endregion

        #region DeleteBYPK 
        public void DeleteBYPK(string conn, int UserID, string StoreProcedure, string parameterName, int ID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand(StoreProcedure);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, parameterName, SqlDbType.Int, ID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception e)
            {
            }
        }
        #endregion
    }
}