using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Data;
using System.Data.Common;

namespace KevalThemeAddressBook.DAL
{
    public class DeleteSelectAll_DAL
    {
        #region SelectAll 
        public DataTable SelectAll(string conn,int UserID,string StoreProcedure)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand(StoreProcedure);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
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

        #region DeleteBYPK 
        public void DeleteBYPK(string conn, int UserID, string StoreProcedure, string parameterName,int ID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand(StoreProcedure);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(dbCMD, parameterName, SqlDbType.Int, ID);
                sqlDB.ExecuteNonQuery(dbCMD);
            }
            catch (Exception e) { 
            }
        }
        #endregion
    }
}
