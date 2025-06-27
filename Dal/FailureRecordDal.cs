using Bll;
using Common;
using Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class FailureRecordDal
    {
        public FailureRecordBll failureRecordBll { get; set; }

        public FailureRecordDal(string Setting)
        {
            failureRecordBll = new FailureRecordBll(Setting);
        }

        public bool Connect()
        {
            return failureRecordBll.Connect();
        }

        public void Disconnect()
        {
            failureRecordBll.Disconnect();
        }
        public string GetProductName(FailureRecord failureRecord)
        {
            try
            {
                var productName = failureRecordBll.GetProductName(failureRecord);
                if (productName == null)
                {
                    return string.Empty;
                }
                else
                {
                    return productName.ToString();
                }
            }
            catch 
            {
                return string.Empty;
            }
        }

        public List<string> GetProductFamilies()
        { 
            var dataTable = failureRecordBll.GetProductFamilies();
            return dataTable.AsEnumerable().Select(row => row[0].ToString()).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        }

        public List<ProcessKeyName> GetProcessesBySn(string serial)
        {
            var dt = failureRecordBll.GetFullProcessJsonBySn(serial);
            //将dt中的第一列和第二列转换为ProcessKeyName对象的列表

            if (dt == null || dt.Rows.Count == 0)
            {
                return new List<ProcessKeyName>();
            }

            return dt.AsEnumerable().Select(row => new ProcessKeyName
            {
                Key = row[1].ToString(),
                Name = row[0].ToString()
            }).ToList();
          
        }

        public Dictionary<string, string> ReadPathItem(FailureRecord failureRecord)
        {
            var pathItem = failureRecordBll.ReadPathItem(failureRecord);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (var item in pathItem.items)
            {
                keyValuePairs.Add(item.key, item.name);
            }
            return keyValuePairs;
        }

        public bool AddFailureRecord(FailureRecord failureRecord)
        {
            try
            {
                failureRecordBll.AddFailureRecord(failureRecord);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool DeleteFailureRecord(FailureRecord failureRecord)
        {
            try
            {
                //var fr = failureRecordBll.GetFailureRecord(failureRecord.Id);
                //fr.DeleteUserName = failureRecord.DeleteUserName;
                //fr.DeleteTime = DateTime.Now;
                //failureRecordBll.AddFailureRecord2DeleteTable(fr);
                failureRecordBll.DeleteFailureRecord(failureRecord.Id,failureRecord.DeleteUserName);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool IsSerialNumberExist(string serialNumber)
        {
            if(failureRecordBll.IsSerialNumberExist(serialNumber) >0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsSnValid(string serial)
        {
            var dt =  failureRecordBll.IsSnValid(serial);
            bool exists = dt.Rows.Count > 0
            && Convert.ToInt32(dt.Rows[0]["is_exists"]) == 1;
            return exists;
        }
        public List<FailureRecord> GetFailureRecords(string sql="")
        {
            string query = sql;
            if (string.IsNullOrEmpty(query))
            {
                query = "SELECT * FROM FailureRecord ORDER BY MODIFYTIME DESC";
            }
            return failureRecordBll.GetFailureRecords(query);
        }

        public FailureRecord GetFailureRecord(int id)
        {
            return failureRecordBll.GetFailureRecord(id);
        }
        public List<FailureRecord> GetFailureRecordsByFuzzy(string sn)
        {
            if (string.IsNullOrEmpty(sn))
            {
                return GetFailureRecords(sn);
            }
            else
            {
                string sql = $"SELECT * FROM FailureRecord WHERE SerialNumber LIKE '%{sn}%' ORDER BY ID DESC";
                return failureRecordBll.GetFailureRecords(sql);
            }
        }

        public string GetFullTreePathBySn(string sn)
        {
            var dt = failureRecordBll.GetFullTreeTableBySn(sn);
            if (dt.Rows.Count > 0 && dt.Columns.Contains("full_tree"))
            {
                return dt.Rows[0]["full_tree"]?.ToString() ?? string.Empty;
            }
            else
            {
                // 没查到就返回空字符串或根据需要抛异常
                return string.Empty;
            }
        }
        public FailureRecord GetFailureRecordBySn(string sn)
        { 
            string sql = "SELECT * FROM FailureRecord WHERE SerialNumber = @SerialNumber";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SerialNumber", sn)
            };
            var dataTable = failureRecordBll.ExecuteQuery(sql, parameters);
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

        public bool UpdateFailureRecord(FailureRecord failureRecord)
        {
            try
            {
                failureRecordBll.UpdateFailureRecord(failureRecord);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
