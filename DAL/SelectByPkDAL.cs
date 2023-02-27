using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.DAL
{
    public class SelectByPkDAL
    {
        #region SelectByPk
        public DataTable SelectByPk(string conn, int UserID, string StoreProcedure,string parametername,int? ID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand(StoreProcedure);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, parametername, SqlDbType.Int, ID);
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

    }
}
