using Framework.DTOs;
using Framework.DTOs.WorkManagementDto.CreateWork.CreateWorkIndexDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Controllers.TaskManagement.WorkManagement;
using WebFramework.Models.Shared;

namespace WebFramework.Models.TaskManagement.WorkManagement
{
    public class CreateWorkViewModel : LayoutViewModel, IRef<CreateWorkController>
    {
        public List<PriorityDto> Priorities { get; set; }
    }
}
