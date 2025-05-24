using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DatabaseFactory
    {
        public IDatabase CreateDatabase(string StorageSetting)
        {
            string dbName = StorageSetting.ToLower().Trim();
            IDatabase db = null;
            switch (dbName)
            {
                case "tym":
                    db = new DatabaseTym();
                    break;
                case "sqlserver":
                    db = new DatabaseSqlServer();
                    break;
                default:
                    throw new Exception("StorageSetting is not correct.");
            }
            return db;
        }
    }
}
