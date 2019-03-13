using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.InputModels.QoutationManagement.OrderList
{
    public class OrderBaseInput
    {
        public int Page { get; set; }
        public int NumberItemPerPage { get; set; }
        public string CreateByFilters { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ProductName { get; set; }
        public String ClientName { get; set; }
        public String OrderStatusId { get; set; }
        public String ColumnSortingName { get; set; }
        //asc để sắp xếp tăng dần còn desc để sắp xếp giảm dần
        public String SortingAction { get; set; }
    }
}
