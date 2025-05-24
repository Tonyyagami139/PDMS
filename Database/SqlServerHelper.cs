using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class SqlServerHelper:SqlHelperBase
    {
        public string ConnStr { get; set; }

        private SqlConnection Connection { get; set; }

        public SqlServerHelper(string connectionStr)
        {
            this.ConnStr = connectionStr;
            Connection = new SqlConnection(connectionStr);
        }
        public void Connect()
        {
            if (Connection.State == System.Data.ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        public void Disconnect()
        {
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        public void Dispose()
        {
            Disconnect();
            Connection.Dispose();
        }


        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            Connect(); // 确保连接已打开
            using (var command = new SqlCommand(query, Connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var dataTable = new DataTable();
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
                return dataTable;
            }
        }

        // 执行非查询操作（插入、更新、删除），返回影响的行数
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            Connect(); // 确保连接已打开
            using (var command = new SqlCommand(query, Connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteNonQuery();
            }
        }

        // 执行单一查询，返回结果的第一行第一列
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            Connect(); // 确保连接已打开
            using (var command = new SqlCommand(query, Connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                return command.ExecuteScalar();
            }
        }
        public DataTable RunStorageProcedure(string storageProcName, SqlParameter[] paras)
        {
            Connect();
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(storageProcName, Connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storageProcName;
                cmd.Parameters.AddRange(paras);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
    }
}
