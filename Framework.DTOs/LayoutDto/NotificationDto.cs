using Framework.Models.NotificationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.LayoutDto
{
    public class NotificationDto : IRef<Notification>
    {
        public String Id { get; set; }
        public String Content { get; set; }
        public bool IsRead { get; set; }
        public String Link { get; set; }
        public DateTime? CreationTime { get; set; }
        public String NewClass
        {
            get
            {
                if(IsRead)
                {
                    return "old";
                }
                return "new";
            }
        }
        public String TimeDisplay
        {
            get
            {
                TimeSpan deltaTime = DateTime.Now - CreationTime.Value;
                StringBuilder stringBuilder = new StringBuilder();

                if (deltaTime.Days > 1)
                {
                    return CreationTime.Value.ToString("dd/MM/yyyy lúc hh:mm");
                }
                else if (deltaTime.Days == 1)
                {
                    return CreationTime.Value.ToString("Hôm qua lúc hh:mm");
                }
                else
                {
                    return CreationTime.Value.ToString("Hôm nay lúc hh:mm");
                }
            }
        }

    }
}
