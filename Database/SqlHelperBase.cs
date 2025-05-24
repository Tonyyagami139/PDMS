using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ColumnNameAttribute : Attribute
    {
        public string Name { get; }

        public ColumnNameAttribute(string name)
        {
            Name = name;
        }
    }
    public class SqlHelperBase
    {
        public List<T> ConvertDataTableToClass<T>(DataTable table) where T : new()
        {
            var result = new List<T>();

            // 缓存属性和列名映射
            var propertyMapping = typeof(T).GetProperties()
                .Select(prop => new
                {
                    Property = prop,
                    // 优先使用属性名
                    ColumnName = table.Columns.Contains(prop.Name)
                        ? prop.Name
                        : prop.GetCustomAttributes(typeof(ColumnNameAttribute), false)
                              .Cast<ColumnNameAttribute>()
                              .FirstOrDefault()?.Name
                })
                .Where(mapping => mapping.ColumnName != null) // 排除找不到列的属性
                .ToList();

            foreach (DataRow row in table.Rows)
            {
                var obj = new T();

                foreach (var mapping in propertyMapping)
                {
                    var prop = mapping.Property;
                    var columnName = mapping.ColumnName;

                    if (table.Columns.Contains(columnName) && row[columnName] != DBNull.Value)
                    {
                        try
                        {
                            // 支持可空类型的转换
                            var propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            var safeValue = Convert.ChangeType(row[columnName], propertyType);

                            // 设置属性值
                            prop.SetValue(obj, safeValue);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException($"Error converting column '{columnName}' to property '{prop.Name}'", ex);
                        }
                    }
                }

                result.Add(obj);
            }

            return result;
        }
    }
}
