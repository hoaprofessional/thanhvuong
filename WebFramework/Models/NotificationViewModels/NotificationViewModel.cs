using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFramework.Models.NotificationViewModels
{
    public class NotificationViewModel
    {
        public string StaffId { get; set; }
        public string NotificationTitle { get; set; }
        public string Link { get; set; }
        public String NotificationTime { get; set; }
        public bool CheckStaffId { get; set; }
    }
}
