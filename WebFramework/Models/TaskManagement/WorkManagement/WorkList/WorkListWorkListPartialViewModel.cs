using Framework.DTOs.WorkManagementDto.WorkList.WorkListIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFramework.Models.TaskManagement.WorkManagement.WorkList
{
    public class WorkListWorkListPartialViewModel
    {
        public List<WorkDto> Works { get; set; }
        public List<PriorityDto> Priorities { get; set; }
        public List<WorkStatusDto> WorkStatuses { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public String ColumnSortingName { get; set; }
        public String SortingAction { get; set; }
    }
}
