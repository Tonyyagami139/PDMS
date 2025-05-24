using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public interface IDatabase
    {
        bool Connect();
        void Disconnect();

        DataTable ExecuteQuery(string query, object[] parameters = null);
        object ExecuteScalar(string query, object[] parameters = null);

        DataTable RunStorageProcedure(string storageProcName, object[] paras);
    }
}
