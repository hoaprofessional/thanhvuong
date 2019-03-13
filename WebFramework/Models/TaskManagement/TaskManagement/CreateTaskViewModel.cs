using Framework.DTOs;
using Framework.DTOs.TaskManagementDto.TaskManagement.CreateTask.CreateTaskIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Controllers.TaskManagement.TaskManagement;
using WebFramework.Models.Shared;

namespace WebFramework.Models.TaskManagement.TaskManagement
{
    public class CreateTaskViewModel : LayoutViewModel, IRef<CreateTaskController>
    {
        public List<PriorityDto> Priorities { get; internal set; }
        public List<AssignUserDto> AssignUsers { get; internal set; }
        public WorkDto Work { get; set; }
    }
}
