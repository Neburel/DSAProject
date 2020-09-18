using DbaWebAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DSALib2
{
    public static class DSALib
    {
        private static DbConnection sqlConnection;
        public static DbConnection SqlConnection
        {
            get
            {
                if (DSALib.sqlConnection != null) return sqlConnection;
                throw new DBException("DSALIb was not init");
            }
        }

        public static void Init(DbConnection SqlConnection)
        {
            string sqlCon = @"Data Source=.\SQLEXPRESS;" +
                @"AttachDbFilename=|DataDirectory|\SampleDB.mdf;
                Integrated Security=True;
                Connect Timeout=30;
                User Instance=True";
        }

    }
}
