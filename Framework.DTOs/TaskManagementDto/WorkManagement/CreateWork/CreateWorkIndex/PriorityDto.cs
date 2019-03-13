using Framework.Models.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.WorkManagementDto.CreateWork.CreateWorkIndexDto
{
    public class PriorityDto : IRef<Priority>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
