using Framework.DTOs.AllNotification;
using Framework.DTOs.CommonDto;
using Framework.Models.NotificationManagement;
using Framework.Repositories.NotificationManagement;
using Framework.Services.QoutationManagementService.CommonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services.AllNotificationServices
{
    public interface IAllNotificationIndexService :
        IPagingObject<Notification, AllNotificationIndexFilter>,
        IPagingService<Notification, AllNotificationIndexFilter>
    {
    }
    public class AllNotificationIndexService : 
        PagingService<Notification, AllNotificationIndexFilter>,
        IAllNotificationIndexService
    {
        INotificationRepository notificationRepository;

        public AllNotificationIndexService(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
            PagingObject = this;
        }

        public IQueryable<Notification> GetQuery(AllNotificationIndexFilter filter)
        {
            return notificationRepository
                .GetMulti(x => x.StaffId == filter.StaffId && x.Active == true);
        }
    }
}
