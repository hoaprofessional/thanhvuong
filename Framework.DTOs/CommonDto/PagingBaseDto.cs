using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.CommonDto
{
    public class PagingBaseDto
    {
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string SortingColumnName { get; set; }
        public string SortingAction { get; set; }
    }
}
