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
        #region LOC_Country_SelectForDropDown
        public DataTable LOC_Country_SelectForDropDown(string conn,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_LOC_Country_SelectForDropDownList");
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

        #region dbo.PR_LOC_State_SelectForDropDown
        public DataTable dbo_PR_LOC_State_SelectForDropDown(string conn)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_LOC_State_SelectForDropDown");

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

        #region dbo.PR_LOC_City_SelectForDropDown
        public DataTable dbo_PR_LOC_City_SelectForDropDown(string conn)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_LOC_City_SelectForDropDown");

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


        #region dbo.PR_LOC_State_SelectDropDownByCountryID
        public DataTable dbo_PR_LOC_State_SelectDropDownByCountryID(string conn, int CountryID,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_Loc_State_SelectDropDownByCountryID");
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

        #region LOC_City_SelectDropDownByStateID
        public DataTable LOC_City_SelectDropDownByStateID(string conn, int StateID,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("PR_Loc_State_SelectDropDownByStateID");
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

        #region LOC_Country_SelectByCountryNameCode
        public DataTable LOC_Country_SelectByCountryNameCode(string conn, string? CountryCode, string? CountryName,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_LOC_Country_Filter");
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                sqlDb.AddInParameter(dbCmd, "CountryName", SqlDbType.NVarChar, CountryName);
                sqlDb.AddInParameter(dbCmd, "CountryCode", SqlDbType.VarChar, CountryCode);
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

        #region LOC_State_SelectByStateNameCode
        public DataTable LOC_State_SelectByStateNameCode(string conn, int? CountryID, string? StateName, string? StateCode,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_LOC_State_Filter");
                if (CountryID == 0)
                {
                    sqlDb.AddInParameter(dbCmd, "CountryID", SqlDbType.Int, null);
                }
                else
                {
                    sqlDb.AddInParameter(dbCmd, "CountryID", SqlDbType.Int, CountryID);
                }
                sqlDb.AddInParameter(dbCmd, "StateName", SqlDbType.NVarChar, StateName);
                sqlDb.AddInParameter(dbCmd, "StateCode", SqlDbType.VarChar, StateCode);
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region LOC_City_SelectByCityNameCode
        public DataTable LOC_City_SelectByCityNameCode(string conn, int? CountryID, int? StateID, string? CityName, string? CityCode,int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);

                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_LOC_City_Filter");
                if (CountryID == 0)
                {
                    sqlDb.AddInParameter(dbCmd, "CountryID", SqlDbType.Int, null);
                }
                else
                {
                    sqlDb.AddInParameter(dbCmd, "CountryID", SqlDbType.Int, CountryID);
                }
                if (StateID == 0)
                {
                    sqlDb.AddInParameter(dbCmd, "StateID", SqlDbType.Int, null);
                }
                else
                {
                    sqlDb.AddInParameter(dbCmd, "StateID", SqlDbType.Int, StateID);
                }
             
                sqlDb.AddInParameter(dbCmd, "CityName", SqlDbType.NVarChar, CityName);
                sqlDb.AddInParameter(dbCmd, "CityCode", SqlDbType.VarChar, CityCode);
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
    }
}
