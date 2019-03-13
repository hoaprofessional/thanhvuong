using Framework.Models.UserManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// Chức năng liên quan :
/// + CM.T.2.1.6.1 : Tìm kiếm task
/// + CM.T.2.1.6.2 : Xem task
/// + CM.T.2.1.6.3 : Tạo task
/// + CM.T.2.1.6.4 : cập nhật task
/// + CM.T.2.1.6.5 : Xóa task
/// + CM.T.2.1.6.6 : Cho phép gia hạn task
/// 
/// Màn hình liên quan :
/// + CM.T.F.4 : Danh sách task của công việc
/// + CM.T.F.5 : Tạo task
/// + CM.T.F.6 : Cập nhật task
/// 
/// Schema : "Task"
/// 
/// Người tạo : Hùng-DV
/// Ngày tạo  : 2018-07-15
/// Cập nhật lần cuối : chưa có cập nhật

namespace Framework.Models.TaskManagement
{
    /// <summary>
    /// Bảng task
    /// </summary>
    [Table("Task")]
    public class Task : Auditable
    {
        /// <summary>
        /// Mã task
        /// </summary>
		[Key()]
        [MaxLength(450)]
        public string Id { get; set; }
        /// <summary>
        /// Tên task
        /// </summary>
		[MaxLength(200)]
		public string Name { get; set; }
        /// <summary>
        /// Công việc chứa task
        /// </summary>
		[MaxLength(450)]
		public string WorkId { get; set; }
        /// <summary>
        /// Người được giao
        /// </summary>
		[MaxLength(450)]
		public string AssignToId { get; set; }
        /// <summary>
        /// Trạng thái task
        /// </summary>
		[MaxLength(450)]
		public string TaskStatusId { get; set; }
        /// <summary>
        /// Mức độ ưu tiên
        /// </summary>
        [MaxLength(450)]
        public string PriorityId { get; set; }
        public DateTime DateReception { get; set; }
        public DateTime? FinishDate { get; set; }
        [MaxLength(450)]
        public string AssignerId { get; set; }
        public string Result { get; set; }
        public DateTime Deadline { get; set; }
        /// <summary>
        /// Người được giao
        /// </summary>
        [ForeignKey("AssignToId")]
        public ApplicationUser AssignTo { get; set; }
        [ForeignKey("AssignerId")]
        public ApplicationUser Assigner { get; set; }
        /// <summary>
        /// Trạng thái task
        /// </summary>
        [ForeignKey("TaskStatusId")]
        public TaskStatus TaskStatus { get; set; }
        /// <summary>
        /// Công việc chứa task
        /// </summary>
        [ForeignKey("WorkId")]
        public Work Work { get; set; }
        /// <summary>
        /// Mức độ ưu tiên
        /// </summary>
        [ForeignKey("PriorityId")]
        public Priority Priority { get; set; }
        public int Order { get; set; }
    }
}
