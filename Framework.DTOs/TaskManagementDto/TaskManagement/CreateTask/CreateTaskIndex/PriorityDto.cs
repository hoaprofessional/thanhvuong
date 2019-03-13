using Framework.Models.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.TaskManagementDto.TaskManagement.CreateTask.CreateTaskIndex
{
    public class PriorityDto : IRef<Priority>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
