using KevalThemeAddressBook.Models;
using KevalThemeAddressBook.Areas.MST_ContactCategory.Models;
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
    public class MST_DAL:MST_DALBASE
    {
        int UserID = (int)CommonVariables.UserID();
        #region ContactCategory_DropDownList
        public DataTable ContactCategory_DropDownList()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_SelectForDropDownList");
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

    }
}
