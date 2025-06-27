using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDMS.SyncFusion
{
    public class SpreadsheetModel
    {
        public string Serial { get; set; }
        public string WorkStep { get; set; }
        public Dictionary<string,List<string>> StringData { get; set; }


    }
}
