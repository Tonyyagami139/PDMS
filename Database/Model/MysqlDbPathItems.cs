using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{

    public class MysqlDbPathItems
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public string pathStr { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public string itemStr { get; set; }
        public List<keyname> items { get; set; }

        public MysqlDbPathItems SelfUpdate()
        {
            items = JsonHelper.DeserializeObject<List<keyname>>(itemStr);
            return this;
        }


    }

    public class keyname
    {
        public string key { get; set; }
        public string name { get; set; }
    }

}
