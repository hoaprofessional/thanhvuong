using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Utils
{
    public class CsvExtension
    {
        public List<T> FromStringContent<T>(String content) where T : class
        {
            return null;
        }

        public static String GetContentFromList<T>(List<T> list) where T : class
        {
            StringBuilder builder = new StringBuilder();
            var type = typeof(T);
            var headers = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => x.Name).ToArray<string>();
            builder.Append(String.Join(",", headers));
            builder.AppendLine();
            foreach (var item in list)
            {
                StringBuilder builderRow = new StringBuilder();
                int i = 0;
                foreach (var typename in headers)
                {
                    var o = type.GetProperty(typename).GetValue(item);
                    if (o == null)
                        continue;
                    String value = o.ToString();
                    value = value.Replace("/", "//");
                    value = value.Replace(",", "/,");
                    if (i > 0)
                    {
                        builderRow.Append(",");
                    }
                    builderRow.Append(value);
                    i++;
                }
                builder.Append(builderRow.ToString());
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
