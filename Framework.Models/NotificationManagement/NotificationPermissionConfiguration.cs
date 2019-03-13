using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Models.NotificationManagement
{
    /* bỏ lớp này */ 
    [Table("NotificationPermissionConfiguration")]
    public class NotificationPermissionConfiguration
    {
        [Key]
        [MaxLength(100)]
        public string Id { get; set; }
        /// <summary>
        /// Tên Permission VD RegisterUser
        /// </summary>
        [MaxLength(100)]
        public string PermissionValue { get; set; }
        /// <summary>
        /// Gửi thông qua mã này. notification.send(NotificationCode,"Nội dung thông báo");
        /// thì nó sẽ gửi những người có permission có notificationCode này
        /// </summary>
        [MaxLength(100)]
        public string NotificationCode { get; set; }
        /// <summary>
        /// Mô tả về NotificationCode không có code qua thuộc tính này
        /// </summary>
        [MaxLength(1000)]
        public string NotificationDecription { get; set; }
    }
}
