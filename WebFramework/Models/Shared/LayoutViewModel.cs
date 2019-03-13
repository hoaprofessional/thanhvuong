using Framework.DTOs;
using Framework.DTOs.LayoutDto;
using Framework.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Controllers.Shared;
using WebFramework.Infrastructor;

namespace WebFramework.Models.Shared
{
    public class LayoutViewModel : IRef<LayoutController>
    {
        public String Title { get; set; }
        public String StaffId { get; set; }
        public List<String> Permissions { get; set; }
        /// <summary>
        /// Người dùng hiện tại đang đăng nhập
        /// </summary>
        public ApplicationUser User { get; set; }

        public List<NotificationDto> TopNotifications { get; set; }
        public int NumberOfUnreadNotification { get; set; }

        public bool HasPermission(string permission)
        {
            if(Permissions==null)
            {
                return false;
            }
            return Permissions.Contains(permission);
        }
    }
}
