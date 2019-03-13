using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.InputModels.WorkManagement.WorkList
{
    public class WorkListFilterInput
    {
        public int Page { get; set; }
        public int NumberItemPerPage { get; set; }
        public string FilterByUserName { get; set; }
        public DateTime? FilterByDateBegin { get; set; }
        public String FilterByStatusId { get; set; }
        public String ColumnSort { get; set; }
        //asc để sắp xếp tăng dần còn desc để sắp xếp giảm dần
        public String ColumnSortingName { get; set; }
        public String SortingAction { get; set; }
    }
}
