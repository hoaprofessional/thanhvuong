using Framework.DTOs;
using Framework.DTOs.WorkManagementDto.WorkList.WorkListIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Controllers.TaskManagement.WorkManagement;
using WebFramework.Models.Shared;

namespace WebFramework.Models.TaskManagement.WorkManagement
{
    public class WorkListIndexViewModel : LayoutViewModel, IRef<WorkListController>
    {
        /// <summary>
        /// Tương ứng với ô hiển thị trên giao diện
        /// </summary>
        public List<int> NumbersShowPage { get; set; }
        public List<CreateByFilterDto> CreateByFilters { get; set; }
        public List<WorkStatusDto> WorkStatusesFilters { get; set; }
        public List<PriorityDto> Priorities { get; set; }
    }
}
