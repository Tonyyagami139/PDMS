using Common;
using Database;
using Database.Model;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class FailureRecordBll
    {
        public IDatabase Db { get; set; }
        public FailureRecordBll(string setting)
        {
            DatabaseFactory df = new DatabaseFactory();
            Db = df.CreateDatabase(setting);
        }
        public bool Connect()
        {
            return Db.Connect();
        }

        public void Disconnect()
        {
            Db.Disconnect();
        }
        public object GetProductName(FailureRecord failureRecord)
        {

            string sql = "SELECT name FROM production.product p INNER JOIN batch b ON p.batch_id=b.ID INNER JOIN TREE_ITEM t ON b.item_id = t.id where p.sn=@sn";
            MySqlParameter[] paras = new MySqlParameter[] {
                new MySqlParameter("@sn",failureRecord.SerialNumber),
            };
            return Db.ExecuteScalar(sql, paras).ToString();

        }

        public DataTable GetFullProcessJsonBySn(string serial)
        {
            string query = "get_process_by_sn";
            MySqlParameter[] paras = new MySqlParameter[]
            {
                new MySqlParameter("@in_sn", serial),
            };
            DataTable dt = Db.RunStorageProcedure(query, paras);
            return dt;

        }
        /// <summary>
        /// 调用存储过程，检查 SN 在MYSQL数据库中是否存在
        /// </summary>
        public DataTable IsSnValid(string sn)
        {
            string query = "is_sn_valid";
            MySqlParameter[] paras = new MySqlParameter[]
            {
                new MySqlParameter("@in_sn", sn),
            };
            DataTable dt = Db.RunStorageProcedure(query, paras);
            return dt;
        }
        /// <summary>
        /// 调用存储过程 get_full_tree_by_sn
        /// </summary>
        public DataTable GetFullTreeTableBySn(string sn)
        {
            string procName = "get_full_tree_by_sn";
            MySqlParameter[] paras = new MySqlParameter[]
            {
                new MySqlParameter("@in_sn", MySqlDbType.VarChar, 255)
                {
                    Direction = ParameterDirection.Input,
                    Value = sn
                }
            };

            // DataTable 中会有一列 full_tree完整路径
            return Db.RunStorageProcedure(procName, paras);
        }
        public DataTable GetProductFamilies()
        {
            string query = "SELECT name FROM production.tree_item where flag = 1";
            MySqlParameter[] paras = new MySqlParameter[] { };
            return Db.ExecuteQuery(query, paras);
        }

        public MysqlDbPathItems ReadPathItem(FailureRecord failureRecord)
        {
            MysqlDbPathItems mpf = new MysqlDbPathItems();

            try
            {
                string connectionString = $"Server=192.168.31.140;Database=production;User ID=tym_measure;Password=Measure20160718;Connection Timeout=100;";
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // SQL 查询，带有参数 @id
                    string query = "SELECT path, items FROM production.product_ftp WHERE sn = @sn";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        // 为查询添加参数
                        cmd.Parameters.AddWithValue("@sn", failureRecord.SerialNumber);

                        // 使用 MySqlDataReader 读取数据
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // 循环读取每一行数据
                            reader.Read();
                            // 使用列的索引或列名来获取数据
                            mpf.pathStr = reader.GetString("path");
                            mpf.itemStr = reader.GetString("items");  // 通过列名获取
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error read pathitem query: {ex.Message}");
            }
            return mpf.SelfUpdate();
        }

        public void AddFailureRecord(FailureRecord fr)
        {
            string query = "INSERT INTO FailureRecord (SerialNumber,ProductName,ProductType,ProductFamily,WorkStepProcessName,FailureMode,CreateUserName,CreateTime,ModifyUserName,ModifyTime,Comment,PictureFileName,Status) VALUES (@SerialNumber,@ProductName,@ProductType,@ProductFamily,@WorkStepProcessName,@FailureMode,@CreateUserName,@CreateTime,@ModifyUserName,@ModifyTime,@Comment,@PictureFileName,@Status)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SerialNumber", fr.SerialNumber),
                new SqlParameter("@ProductName", fr.ProductName),
                new SqlParameter("@ProductType", fr.ProductType),
                new SqlParameter("@ProductFamily", fr.ProductFamily),
                new SqlParameter("@WorkStepProcessName", fr.WorkStepProcessName),
                new SqlParameter("@FailureMode", fr.FailureMode),
                new SqlParameter("@CreateUserName", fr.CreateUserName),
                new SqlParameter("@CreateTime", fr.CreateTime),
                new SqlParameter("@ModifyUserName", fr.ModifyUserName),
                new SqlParameter("@ModifyTime", fr.ModifyTime),
                new SqlParameter("@Comment", fr.Comment),
                new SqlParameter("@PictureFileName", fr.PictureFileName),
                new SqlParameter("@Status", fr.Status)
            };
            Db.ExecuteQuery(query, parameters);
        }

        public void DeleteFailureRecord(int Id,string DeleteUserName)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@DeleteUserName", DeleteUserName)
            };
            Db.RunStorageProcedure("dbo.sp_DeleteFailureRecord", parameters);
        }
        public int IsSerialNumberExist(string sn)
        {
            string query = "SELECT COUNT(*) FROM FailureRecord WHERE SerialNumber = @SerialNumber";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SerialNumber", sn)
            };
            return Convert.ToInt32(Db.ExecuteScalar(query, parameters));         
        }

        public List<FailureRecord> GetFailureRecords(string sql)
        {
            List<FailureRecord> failureRecords = new List<FailureRecord>();
            string query = sql;
            var dataTable = Db.ExecuteQuery(query);
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                FailureRecord fr = new FailureRecord();
                fr.Id = Convert.ToInt32(row["Id"]);
                fr.SerialNumber = row["SerialNumber"].ToString();
                fr.ProductName = row["ProductName"].ToString();
                fr.ProductType = row["ProductType"].ToString();
                fr.ProductFamily = row["ProductFamily"].ToString();
                fr.WorkStepProcessName = row["WorkStepProcessName"].ToString();
                fr.FailureMode = row["FailureMode"].ToString();
                fr.CreateUserName = row["CreateUserName"].ToString();
                fr.CreateTime = Convert.ToDateTime(row["CreateTime"]);
                fr.ModifyUserName = row["ModifyUserName"].ToString();
                fr.ModifyTime = Convert.ToDateTime(row["ModifyTime"]);
                fr.Comment = row["Comment"].ToString();
                fr.PictureFileName = row["PictureFileName"].ToString();
                fr.Status = row["Status"].ToString();
                failureRecords.Add(fr);
            }
            return failureRecords;
        }
        public FailureRecord GetFailureRecord(int id)
        { 
            string sql = "SELECT * FROM FailureRecord WHERE Id = @Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };
            var dataTable = Db.ExecuteQuery(sql, parameters);
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                FailureRecord fr = new FailureRecord();
                fr.Id = Convert.ToInt32(dataTable.Rows[0]["Id"]);
                fr.SerialNumber = dataTable.Rows[0]["SerialNumber"].ToString();
                fr.ProductName = dataTable.Rows[0]["ProductName"].ToString();
                fr.ProductType = dataTable.Rows[0]["ProductType"].ToString();
                fr.ProductFamily = dataTable.Rows[0]["ProductFamily"].ToString();
                fr.WorkStepProcessName = dataTable.Rows[0]["WorkStepProcessName"].ToString();
                fr.FailureMode = dataTable.Rows[0]["FailureMode"].ToString();
                fr.CreateUserName = dataTable.Rows[0]["CreateUserName"].ToString();
                fr.CreateTime = Convert.ToDateTime(dataTable.Rows[0]["CreateTime"]);
                fr.ModifyUserName = dataTable.Rows[0]["ModifyUserName"].ToString();
                fr.ModifyTime = Convert.ToDateTime(dataTable.Rows[0]["ModifyTime"]);
                fr.Comment = dataTable.Rows[0]["Comment"].ToString();
                fr.PictureFileName = dataTable.Rows[0]["PictureFileName"].ToString();
                fr.Status = dataTable.Rows[0]["Status"].ToString();
                return fr;
            }
        }
        
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            return Db.ExecuteQuery(query, parameters);
        }

        public void UpdateFailureRecord(FailureRecord fr)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", fr.Id),
                new SqlParameter("@SerialNumber", fr.SerialNumber),
                new SqlParameter("@ProductName", fr.ProductName),
                new SqlParameter("@ProductType", fr.ProductType),
                new SqlParameter("@ProductFamily", fr.ProductFamily),
                new SqlParameter("@WorkStepProcessName", fr.WorkStepProcessName),
                new SqlParameter("@FailureMode", fr.FailureMode),
                new SqlParameter("@CreateUserName", fr.CreateUserName),
                new SqlParameter("@CreateTime", fr.CreateTime),
                new SqlParameter("@ModifyUserName", fr.ModifyUserName),
                new SqlParameter("@ModifyTime", fr.ModifyTime),
                new SqlParameter("@Comment", fr.Comment),
                new SqlParameter("@PictureFileName", fr.PictureFileName),
                new SqlParameter("@Status", fr.Status)
            };

            Db.RunStorageProcedure("dbo.sp_ModifyFailureRecord", parameters);

        }
    }
}
