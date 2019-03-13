using Framework.DTOs.CommonDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.AllNotification
{
    public class AllNotificationIndexFilter : PagingBaseDto
    {
        public string StaffId { get; set; }
    }
}
