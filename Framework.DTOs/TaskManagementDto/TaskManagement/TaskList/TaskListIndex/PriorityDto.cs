using Framework.Models.TaskManagement;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.TaskManagementDto.TaskManagement.TaskList.TaskListIndex
{
    public class PriorityDto : IRef<Priority>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
