using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using KevalThemeAddressBook.BAL;

namespace KevalThemeAddressBook.DAL
{
    public class LOC_DAL : LOC_DALBASE
    {

        int UserID = (int)CommonVariables.UserID();
        #region LOC_Country_SelectForDropDownListByUserID
        public DataTable LOC_Country_SelectForDropDownListByUserID()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectForDropDownListByUserID");
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

        #region LOC_State_SelectForDropDownListByUserID
        public DataTable LOC_State_SelectForDropDownListByUserID(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_SelectForDropDownListByUserID");

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

        #region LOC_City_SelectForDropDownListByUserID
        public DataTable LOC_City_SelectForDropDownListByUserID(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_SelectForDropDownListByUserID");

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

        #region LOC_State_SelectDropDownByCountryIDUserID
        public DataTable LOC_State_SelectDropDownByCountryIDUserID(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_SelectStateDropDownByCountryIDUserID");
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

        #region LOC_City_SelectDropDownByStateIDUserID
        public DataTable LOC_City_SelectDropDownByStateIDUserID(int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectCityDropDownByStateIDUserID");
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

        #region LOC_Country_SelectByCountryNameCodeByUserID
        public DataTable LOC_Country_SelectByCountryNameCodeByUserID(string? CountryCode, string? CountryName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_SelectByCountryNameCountryCodeByUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.VarChar, CountryCode);
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

        #region LOC_State_SelectByStateNameCodeUserID
        public DataTable LOC_State_SelectByStateNameCodeUserID(int? CountryID, string? StateName, string? StateCode)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_SelectByCountryStateNameStateCodeUserID");
                if (CountryID == 0)
                {
                    sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, null);
                }
                else
                {
                    sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                }
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.VarChar, StateCode);
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region LOC_City_SelectByCityNameCodeUserID
        public DataTable LOC_City_SelectByCityNameCodeUserID(int? CountryID, int? StateID, string? CityName, string? CityCode)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);

                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_SelectByCountryStateCityNameUserID");
                if (CountryID == 0)
                {
                    sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, null);
                }
                else
                {
                    sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                }
                if (StateID == 0)
                {
                    sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, null);
                }
                else
                {
                    sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                }

                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.VarChar, CityCode);
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
    }
}
