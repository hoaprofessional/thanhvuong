using Framework.DTOs;
using Framework.DTOs.TaskManagementDto.TaskManagement.TaskList.TaskListIndex;
using Framework.Models.TaskManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Controllers.TaskManagement.TaskManagement;
using WebFramework.Models.Shared;

namespace WebFramework.Models.TaskManagement.TaskManagement
{
    public class TaskListIndexViewModel : LayoutViewModel, IRef<TaskListController>
    {
        public Work Work { get; set; }
        public bool CanCreateTask { get; set; }
        public string CurrentUserId { get; set; }
        public List<PriorityDto> Priorities { get; set; }
        public List<AssignUserDto> AssignToUsers { get; set; }
        public List<TaskStatusDto> TaskStatuss { get; set; }

    }
}
