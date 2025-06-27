using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Database
{

    public class MySqlHelper:SqlHelperBase
    {
        private MySqlConnection connection;
        private string connectionString;

        public MySqlHelper(string server, string database, string username, string password)
        {
            // 初始化连接字符串
            connectionString = $"Server={server};Database={database};User ID={username};Password={password};Connection Timeout=100;";
            connection = new MySqlConnection(connectionString);
        }

        // 打开数据库连接
        public void OpenConnection()
        {

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();

            }

        }

        // 关闭数据库连接
        public void CloseConnection()
        {

            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }

        }

        

        // 执行查询
        public DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OpenConnection();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing query: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }

        // 执行非查询操作 (如 INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, MySqlParameter[] paras)
        {
            int rowsAffected = 0;
            try
            {
                OpenConnection();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddRange(paras);
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing non-query: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
            return rowsAffected;
        }

        public object ExecuteScalar(string query, MySqlParameter[] paras)
        {
            object result = null;
            try
            {
                OpenConnection();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddRange(paras);
                    result = command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing non-query: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }

        public DataTable RunStorageProcedure(string storageProcName, MySqlParameter[] paras)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OpenConnection();
                using (MySqlCommand command = new MySqlCommand(storageProcName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (paras != null && paras.Length > 0)
                    {
                        command.Parameters.AddRange(paras);
                    }
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing stored procedure: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }

    }
}