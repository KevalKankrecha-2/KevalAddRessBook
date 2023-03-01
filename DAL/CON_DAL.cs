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
        #region ontact_Filter
        public DataTable Contact_Filter(string conn, int? CountryID, int? StateID, int? CityID, string? ContactName, int UserID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(conn);
                DbCommand dbCmd = sqlDb.GetStoredProcCommand("dbo.PR_Contact_Filter");
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
                if (CityID == 0)
                {
                    sqlDb.AddInParameter(dbCmd, "CityID", SqlDbType.Int,  null);
                }
                else
                {
                    sqlDb.AddInParameter(dbCmd, "CityID", SqlDbType.Int, CityID);
                }
                sqlDb.AddInParameter(dbCmd, "UserID", SqlDbType.Int, UserID);
                sqlDb.AddInParameter(dbCmd, "ContactName", SqlDbType.NVarChar, ContactName);

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
    }
}
