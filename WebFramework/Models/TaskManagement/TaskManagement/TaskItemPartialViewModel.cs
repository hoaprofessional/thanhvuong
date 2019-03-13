using Framework.DTOs.TaskManagementDto.TaskManagement.TaskList.TaskListIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFramework.Models.TaskManagement.TaskManagement
{
    public class TaskItemPartialViewModel
    {
        public Framework.Models.TaskManagement.Task Task { get; set; }
        public string CurrentUserName { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsNewTask { get; set; }
        public bool CanCreateTask { get; set; }
        public List<AssignUserDto> AssignToUsers { get; set; }
        public List<PriorityDto> Priorities { get; set; }
        public List<TaskStatusDto> TaskStatuss { get; set; }

        public bool IsAssigner { get; set; }
        public bool IsPerformer { get; set; }

    }
}
