using Framework.Models.TaskManagement;

namespace Framework.DTOs.TaskManagementDto.TaskManagement.CreateTask.CreateTaskIndex
{
    public class WorkDto : IRef<Work>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
