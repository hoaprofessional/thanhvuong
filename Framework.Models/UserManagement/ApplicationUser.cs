using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Framework.Models.UserManagement
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : 
        IdentityUser,
        IAuditable
    {
        /// <summary>
        /// Tên của user
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Thời điểm tạo
        /// </summary>
        public DateTime? CreationTime { get; set; }
        /// <summary>
        /// Avatar của user
        /// </summary>
        public String Avatar { get; set; }
        /// <summary>
        /// Địa chỉ của user
        /// </summary>
        public String Address { get; set; }
        /// <summary>
        /// Nghề nghiệp của user
        /// </summary>
        public String Carrer { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public String CreationUserName { get; set; }
        /// <summary>
        /// Thời điểm chỉnh sửa cuối cùng
        /// </summary>
        public DateTime? ModifiedTime { get; set; }
        /// <summary>
        /// Người chỉnh sửa cuối cùng
        /// </summary>
        public String ModifiedUserName { get; set; }
        /// <summary>
        /// Dòng này có được hoạt động hay không. Nếu Active = null hoặc false thì dòng này coi như đã xóa
        /// </summary>
        public bool? Active { get; set; }
        /// <summary>
        /// Ghi chú thêm cho dòng
        /// </summary>
        public String Note { get; set; }
        /// <summary>
        /// User này có bị cấm hoạt động hay không
        /// </summary>
        public bool? IsBanned { get; set; }
        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public String IdentityCardNumber { get; set; }
        /// <summary>
        /// Dòng được bảo vệ chỉ những người có quyền super user mới có quyền xóa
        /// </summary>
        public bool? Protected { get; set; }
        /// <summary>
        /// Dòng được thêm vào để mục đích test không hiển thị lên giao diện
        /// </summary>
        public bool? IsTest { get; set; }
        /// <summary>
        /// Mã đối tượng
        /// </summary>
        [MaxLength(450)]
        public String ObjectId { get; set; }

        [ForeignKey("ObjectId")]
        public UserObject UserObject { get; set; }

    }
}
