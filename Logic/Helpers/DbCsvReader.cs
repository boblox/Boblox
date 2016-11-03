using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Logic.Helpers
{
    public class CsvConverter<TEntity>
    {
        public static byte[] Convert(IList<TEntity> data)
        {
            var result = new StringBuilder();
            var type = typeof(TEntity);
            var properties = type.GetProperties();
            var headers = new List<KeyValuePair<PropertyInfo, string>>();
            foreach (var property in properties)
            {
                var name = null as string;
                var attributes = property.GetCustomAttributes(false);
                var notMappedAttr = attributes.OfType<NotMappedAttribute>().LastOrDefault();
                if (notMappedAttr == null)
                {
                    var displayAttr = attributes.OfType<DisplayAttribute>().LastOrDefault();
                    if (displayAttr != null)
                    {
                        name = displayAttr.GetName();
                    }
                    headers.Add(new KeyValuePair<PropertyInfo, string>(property, name ?? property.Name));
                }
            }
            result.Append(GetCsvLine(headers.Select(h => h.Value).Cast<object>().ToList()));

            foreach (var item in data)
            {
                result.Append(ObjectToCsvData(item, headers.Select(p => p.Key).ToList()));
            }

            return Encoding.UTF8.GetBytes(result.ToString());
        }

        #region Helpers
        public static string GetCsvLine(IList<object> values)
        {
            var result = new StringBuilder();
            for (var index = 0; index < values.Count; index++)
            {
                var value = values[index] == null ? string.Empty : values[index].ToString();
                value = value.Replace("\"", "\"\"");
                result.Append("\"" + value + "\"");
                result.Append(index < values.Count - 1 ? "," : "\r\n");
            }
            return result.ToString();
        }

        public static string ObjectToCsvData(object obj, IList<PropertyInfo> properties)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "Value can not be null or Nothing!");
            }
            var values = properties.Select(p => p.GetValue(obj, null)).ToList();
            return GetCsvLine(values);
        }

        #endregion
    }
}
