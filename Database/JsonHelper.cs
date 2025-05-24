using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Database
{
    public class JsonHelper
    {
        public static JsonSerializer _serializer;

        public static JsonSerializer CustomSerializer
        {
            get
            {
                if (_serializer != null)
                    return _serializer;

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    FloatFormatHandling = FloatFormatHandling.String, // to allow NaN, Inf, etc
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Newtonsoft.Json.Formatting.Indented
                };

                settings.Converters.Add(new StringEnumConverter());
                settings.Converters.Add(new TimeSpanConverter());

                _serializer = JsonSerializer.Create(settings);

                return _serializer;
            }
        }

        public static string SerializeObject(object target)
        {
            using (StreamWriter sw = new StreamWriter(new MemoryStream()))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                CustomSerializer.Serialize(jw, target);
                jw.Flush();
                //return Encoding.Default.GetString(((MemoryStream)sw.BaseStream).ToArray());
                return Encoding.UTF8.GetString(((MemoryStream)sw.BaseStream).ToArray());
            }
        }

        public static T DeserializeObject<T>(string json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                return CustomSerializer.Deserialize<T>(jr);
            }
        }

        public static T CreateFromFile<T>(string fileName)
        {
            using (StreamReader sr = File.OpenText(fileName))
            {
                string json = sr.ReadToEnd();

                T obj = JsonHelper.DeserializeObject<T>(json);

                return obj;
            }
        }

        public static void SaveToFile(string fileName, object obj)
        {
            using (StreamWriter w = new StreamWriter(File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None)))
            {
                w.Write(JsonHelper.SerializeObject(obj));
            }
        }
    }


    /// <summary>
    /// https://en.wikipedia.org/wiki/ISO_8601#Durations
    /// </summary>
    public class TimeSpanConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var ts = (TimeSpan)value;
            var tsString = XmlConvert.ToString(ts);
            serializer.Serialize(writer, tsString);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var value = serializer.Deserialize<String>(reader);
            return XmlConvert.ToTimeSpan(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
        }
    }
}

