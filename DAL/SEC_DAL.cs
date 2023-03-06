using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace KevalThemeAddressBook.DAL
{
    public class SEC_DAL:DALHelper
    {
        #region SEC_LoginSelectByUserNamePassword
        public DataTable SEC_LoginSelectByUserNamePassword(string UserName,string Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_User_Master_SelectByNamePassword");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, UserName);
                    sqlDB.AddInParameter(dbCMD, "UserPassword", SqlDbType.VarChar, Password);
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
