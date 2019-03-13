using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Models.NotificationManagement
{
    public class QoutationSendNotificationConfig : Auditable
    {
        [Key]
        [MaxLength(450)]
        public string Id { get; set; }

        public string SendtoPermission { get; set; }
        public string QoutationStatus { get; set; }
    }
}
