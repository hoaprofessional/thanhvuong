using Framework.Models.TaskManagement;

namespace Framework.DTOs.WorkManagementDto.WorkList.WorkListIndex
{
    public class PriorityDto : IRef<Priority>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
