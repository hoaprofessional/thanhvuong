using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utils
{
    public class PagingResultDto<T>
    {
        public int PageIndex { get; set; }
        public int TotalRow { get; set; }
        public int TotalPage { get; set; }
        public List<T> Items { get; set; }
    }
}
