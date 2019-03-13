using Framework.Models.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.WorkManagementDto.WorkList.WorkListIndex
{
    public class WorkStatusDto : IRef<WorkStatus>
    {
        public string Id { get; set; }
        public string Decription { get; set; }
    }
}
