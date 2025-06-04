using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FailureRecord
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }

        public  string ProductFamily { get; set; }
        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public string WorkStepProcessName { get; set; }
        public string FailureMode { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CreateUserName { get; set; }
        public DateTime CreateTime { get; set; }
        public string ModifyUserName { get; set; }
        public DateTime ModifyTime { get; set; }
        public string Comment { get; set; }
        public string PictureFileName { get; set; }
        public string DeleteUserName { get; set; }
        public DateTime DeleteTime { get; set; }

    }
}
