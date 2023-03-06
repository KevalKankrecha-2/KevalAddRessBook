using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.DAL
{
    public class LOC_DAL:LOC_DALBASE
    {
        int UserID = 1;
        #region LOC_Country_SelectForDropDown
        public DataTable LOC_Country_SelectForDropDown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectForDropDownList");
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

        #region LOC_State_SelectForDropDown
        public DataTable dbo_PR_LOC_State_SelectForDropDown(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_SelectForDropDown");

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

        #region LOC_City_SelectForDropDown
        public DataTable dbo_PR_LOC_City_SelectForDropDown(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_SelectForDropDown");

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

        #region LOC_State_SelectDropDownByCountryID
        public DataTable dbo_PR_LOC_State_SelectDropDownByCountryID(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_SelectStateDropDownByCountryID");
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

        #region LOC_City_SelectDropDownByStateID
        public DataTable LOC_City_SelectDropDownByStateID(int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectCityDropDownByStateID");
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

        #region LOC_Country_SelectByCountryNameCode
        public DataTable LOC_Country_SelectByCountryNameCode(string? CountryCode, string? CountryName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_SelectByCountryNameCountryCode");
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

        #region LOC_State_SelectByStateNameCode
        public DataTable LOC_State_SelectByStateNameCode(int? CountryID, string? StateName, string? StateCode)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_SelectByCountryStateNameStateCode");
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

        #region LOC_City_SelectByCityNameCode
        public DataTable LOC_City_SelectByCityNameCode(int? CountryID, int? StateID, string? CityName, string? CityCode)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);

                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_SelectByCountryStateCityName");
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
