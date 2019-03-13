using Framework.DTOs.CommonDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.TaskManagementDto.WorkManagement.WorkList.WorkListIndex
{
    public class WorkFilterDto : PagingBaseDto
    {
        public string UserNameFilter { get; set; }
        public DateTime? DateBeginFilter { get; set; }
        public String StatusIdFilter { get; set; }
        public String CreationUserName { get; set; }
    }
}
