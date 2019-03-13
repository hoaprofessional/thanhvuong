using Framework.DTOs;
using Framework.Models.NotificationManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.InputModels.HomePage
{
    public class NotificationInput : IRef<Notification>
    {
        [Required]
        public String Content { get; set; }
        [Required]
        public String Link { get; set; }
        public bool IsSelf { get; set; }
        public string StaffId { get; set; }
        public string Permission { get; set; }
    }
}
