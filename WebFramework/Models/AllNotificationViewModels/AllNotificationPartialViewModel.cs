using Framework.Models.NotificationManagement;
using System.Collections.Generic;

namespace WebFramework.Models.AllNotificationViewModels
{
    public class AllNotificationPartialViewModel
    {
        public int CurrentPage { get; set; }
        public int NumberOfPages { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
