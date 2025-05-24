using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Database;
using Common;

namespace Bll
{
    public class UserBll
    {
        public IDatabase Db { get; set; }
        public UserBll(string setting)
        { 
            DatabaseFactory df = new DatabaseFactory();
            Db = df.CreateDatabase(setting);
        }
        public string Login(string username)
        {
            string query = $"SELECT PASSWORD FROM user WHERE ACCOUNT = @USERNAME";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@USERNAME", username)
            };
            DataTable dt = Db.ExecuteQuery(query, parameters);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            return dt.Rows[0]["password"].ToString();
        }

        public string GetAccessLevel(string username)
        {
            string query = $"SELECT rights FROM user WHERE ACCOUNT = @USERNAME";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@USERNAME", username)
            };
            var right = Db.ExecuteScalar(query, parameters);
            return right.ToString();

        }
        
    }
}
