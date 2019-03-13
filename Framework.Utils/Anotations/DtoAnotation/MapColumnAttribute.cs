using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utils.Anotations.DtoAnotation
{
    public class MapColumnAttribute : Attribute
    {

        public MapColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }

        public string ColumnName { get; set; }
    }
}
