using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Models
{
    public interface IAuditable
    {
        /// <summary>
        /// Thời điểm tạo
        /// </summary>
        DateTime? CreationTime { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        String CreationUserName { get; set; }
        /// <summary>
        /// Thời điểm chỉnh sửa cuối cùng
        /// </summary>
        DateTime? ModifiedTime { get; set; }
        /// <summary>
        /// Người chỉnh sửa cuối cùng
        /// </summary>
        String ModifiedUserName { get; set; }
        /// <summary>
        /// Dòng này có được hoạt động hay không. Nếu Active = null hoặc false thì dòng này coi như đã xóa
        /// </summary>
        bool? Active { get; set; }
        /// <summary>
        /// Dòng được bảo vệ chỉ những người có quyền super user mới có quyền xóa
        /// </summary>
        bool? Protected { get; set; }
        /// <summary>
        /// Dòng được thêm vào để mục đích test không hiển thị lên giao diện
        /// </summary>
        bool? IsTest { get; set; }
        /// <summary>
        /// Ghi chú thêm cho dòng
        /// </summary>
        String Note { get; set; }
    }

    public class Auditable : IAuditable
    {
        /// <summary>
        /// Thời điểm tạo
        /// </summary>
        public DateTime? CreationTime { get; set; }
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
        /// Dòng được bảo vệ chỉ những người có quyền super user mới có quyền xóa
        /// </summary>
        public bool? Protected { get; set; }
        /// <summary>
        /// Dòng được thêm vào để mục đích test không hiển thị lên giao diện
        /// </summary>
        public bool? IsTest { get; set; }
    }
}
