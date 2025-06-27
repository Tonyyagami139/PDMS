using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DatabaseTym : IDatabase
    {
        public MySqlHelper msh { get; set; }
        public FtpHelper fh { get; set; }

        public DatabaseTym()
        {
            //msh = new DbTool.MySqlHelper("192.168.31.140", "production", "tym", "20160718");
            //msh = new MySqlHelper("192.168.31.140", "production", "tym_measure", "Measure20160718");
            msh = new MySqlHelper("192.168.31.140", "production", "zTxTest", "Zhang1121");
            fh = new FtpHelper("192.168.31.140", 210, "tym", "20160718");
        }
        public bool Connect()
        {
            try
            {
                msh.OpenConnection();
                fh.Connect();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void Disconnect()
        {
            msh.CloseConnection();
            fh.Disconnect();
        }

        public DataTable ExecuteQuery(string query, object[] parameters = null)
        {
            return msh.ExecuteQuery(query, (MySqlParameter[])parameters);
        }

        public object ExecuteScalar(string query, object[] parameters = null)
        {
            return msh.ExecuteScalar(query, (MySqlParameter[])parameters);
        }

        public DataTable RunStorageProcedure(string storageProcName, object[] paras)
        {

            var parameters = (MySqlParameter[])paras;
            return msh.RunStorageProcedure(storageProcName, parameters);
        }
    }
}
