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

namespace KevalThemeAddressBook.DAL
{
    public class LOC_DALBASE:DALHelper
    {
        int UserID = 1;
        #region LOC_Country_SelectAll
        public DataTable LOC_Country_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_SelectAll");
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

        #region LOC_State_SelectAll
        public DataTable LOC_State_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectAll");
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

        #region LOC_City_SelectAll
        public DataTable LOC_City_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectAll");
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

        #region LOC_Country_SelectByPK
        public DataTable LOC_Country_SelectByPK(int? CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectByPK");
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

        #region LOC_State_SelectByPK
        public DataTable LOC_State_SelectByPK(int? StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_Loc_State_SelectBYPK");
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

        #region LOC_City_SelectByPK
        public DataTable LOC_City_SelectByPK(int? CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectByPK");
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

        #region LOC_Country_Insert
        public string LOC_Country_Insert(LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.VarChar, modelLOC_Country.CountryCode);
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

        #region LOC_State_Insert
        public string LOC_State_Insert(LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_State.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, modelLOC_State.StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.VarChar, modelLOC_State.StateCode);
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "UserId", SqlDbType.Int, UserID);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_City_Insert
        public string LOC_City_Insert(LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.VarChar, modelLOC_City.CityCode);
                sqlDB.AddInParameter(dbCMD, "CreationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.DateTime, DBNull.Value);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);

                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_Country_UpdateByPK
        public string LOC_Country_UpdateByPK(LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_Country.CountryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.VarChar, modelLOC_Country.CountryCode);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.Date, DBNull.Value);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_State_UpdateByPK
        public string LOC_State_UpdateByPK(LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_State.CountryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_State.StateID);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, modelLOC_State.StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.VarChar, modelLOC_State.StateCode);
                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.Date, DBNull.Value);
                sqlDB.ExecuteNonQuery(dbCMD);
                return null;
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion

        #region LOC_City_UpdateByPK
        public void LOC_City_UpdateByPK(LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelLOC_City.CityID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.VarChar, modelLOC_City.CityCode);

                sqlDB.AddInParameter(dbCMD, "ModificationTime", SqlDbType.Date, DBNull.Value);

                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception ex) { }

        }
        #endregion

        #region LOC_CityDeleteBYPK 
        public void LOC_CityDeleteBYPK(int CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception e)
            {
            }
        }
        #endregion

        #region LOC_CountryDeleteByPK 
        public void LOC_CountryDeleteByPK(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception e)
            {
            }
        }
        #endregion

        #region LOC_StateDeleteByPK 
        public void LOC_StateDeleteByPK(int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception e)
            {
            }
        }
        #endregion
    }
}