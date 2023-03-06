using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.DAL
{
    public class CON_DAL : CON_DALBASE
    {
        int UserID=1;
        #region Contact_Filter
        public DataTable Contact_Filter(int? CountryID, int? StateID, int? CityID, string? ContactName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_CON_Contact_SelectByCountryStateCityContactName");
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
                if (CityID == 0)
                {
                    sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int,  null);
                }
                else
                {
                    sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                }
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, "ContactName", SqlDbType.NVarChar, ContactName);

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
    }
}
