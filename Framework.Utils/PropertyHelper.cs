using Framework.Utils.Anotations.DtoAnotation;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Utils
{
    public static class PropertyHelper
    {
        public static void CopyFrom<S,D>(this D dest,S source)
        {
            var dType = typeof(D);
            var sType = typeof(S);

            var sProperties = sType.GetProperties();
            foreach(var sProperty in sProperties)
            {
                var name = sProperty.Name;
                var mapColumn = (MapColumnAttribute)sProperty
                    .GetCustomAttributes(typeof(MapColumnAttribute), false)
                    .SingleOrDefault();
                if (mapColumn != null)
                {
                    name = mapColumn.ColumnName;
                }
                var dProperty = dType.GetProperty(name);
                if (dProperty == null)
                    continue;
                var srcValue = sProperty.GetValue(source);
                if (srcValue == null)
                    continue;
                dProperty.SetValue(dest, srcValue);
            }
        }
    }
}
