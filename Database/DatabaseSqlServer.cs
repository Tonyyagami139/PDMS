using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections;

namespace Database
{
    public class DatabaseSqlServer:IDatabase
    {
        private SqlServerHelper SqlHelper { get; set; }

        private FtpHelper MyFtpHelper { get; set; }



        //File server : user:sa pwd:Pass100word ==> \\192.168.31.223\TestingDataRoot

        public DatabaseSqlServer()
        {
            string ConnectionStr = @"Data Source = 192.168.31.223;Initial Catalog = PDMS;User Id = sa;Password = Pass100word;";//may need this ? : Connection TimeOut = 100;
            SqlHelper = new SqlServerHelper(ConnectionStr);
            MyFtpHelper = new FtpHelper("192.168.31.223", 21, "sa", "Pass100word");


        }
        public bool Connect()
        {
            try
            {
                MyFtpHelper.Connect();
                SqlHelper.Connect();
                return true;
            }
            catch { return false; }
        }

        public void Disconnect()
        {
            MyFtpHelper?.Disconnect();
            SqlHelper?.Disconnect();
        }

        public DataTable ExecuteQuery(string query, object[] parameters = null)
        {
            return SqlHelper.ExecuteQuery(query, (SqlParameter[])parameters);
        }

        public object ExecuteScalar(string query, object[] parameters = null)
        {
           return SqlHelper.ExecuteScalar(query, (SqlParameter[])parameters);
        }

        public DataTable RunStorageProcedure(string storageProcName, object[] paras)
        {
            var parameters = (SqlParameter[])paras;
            return SqlHelper.RunStorageProcedure(storageProcName, parameters);
        }
    }
}
