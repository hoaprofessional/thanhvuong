using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Models.NotificationManagement
{
    public class Notification : Auditable
    {
        [Key()]
        [MaxLength(450)]
        public String Id { get; set; }
        public String Content { get; set; }
        public String StaffId { get; set; }
        public String Link { get; set; }
        public String Icon { get; set; }
        public bool IsRead { get; set; }
    }
}
