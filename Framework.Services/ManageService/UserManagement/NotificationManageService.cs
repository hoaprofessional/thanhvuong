using Framework.Models.NotificationManagement;
using Framework.Models.TaskManagement;
using Framework.Repositories.NotificationManagement;
using Framework.Repositories.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Services.ManageService.TaskManagement
{
    public interface INotificationManageService : IManageServiceBase<Notification>
    {

    }
    public class NotificationManageService : ManageServiceBase<Notification>, INotificationManageService
    {
        public NotificationManageService(INotificationRepository repository)
            : base(repository)
        {
        }
    }
}
