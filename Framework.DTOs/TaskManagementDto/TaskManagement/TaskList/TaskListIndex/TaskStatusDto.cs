using Framework.Models.TaskManagement;

namespace Framework.DTOs.TaskManagementDto.TaskManagement.TaskList.TaskListIndex
{
    public class TaskStatusDto : IRef<TaskStatus>
    {
        public string Id { get; set; }
        public string Decription { get; set; }
    }
}
