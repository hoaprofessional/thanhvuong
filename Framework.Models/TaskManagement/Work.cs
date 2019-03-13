using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// Chức năng liên quan :
/// + CM.T.2.1.1 Xem công việc
/// + CM.T.2.1.2 Tìm kiếm công việc
/// + CM.T.2.1.3 Tạo công việc mới
/// + CM.T.2.1.4 Cập nhật thông tin công việc
/// + CM.T.2.1.5 Xóa công việc
/// 
/// Màn hình liên quan :
/// + CM.T.F.1 Trang xem công việc
/// + CM.T.F.2 Trang tạo công việc
/// + CM.T.F.3 Trang cập nhật công việc
/// 
/// Schema : "Task"
/// 
/// Người tạo : Hùng-DV
/// Ngày tạo  : 2018-07-15
/// Cập nhật lần cuối : chưa có cập nhật

namespace Framework.Models.TaskManagement
{
    /// <summary>
    /// Công việc (nơi tổng hợp các task)
    /// </summary>
    [Table("Work")]
    public class Work : Auditable
    {
		[Key()]
        [MaxLength(450)]
		public string Id { get; set; }

		[MaxLength(200)]
		public string Name { get; set; }
        public DateTime TimeExpired { get; set; }
        [MaxLength(450)]
        public string PriorityId { get; set; }
        public DateTime DateBegin { get; set; }

        [MaxLength(450)]
        public string WorkStatusId { get; set; }

        [ForeignKey("PriorityId")]
        public Priority Priority { get; set; }

        [ForeignKey("WorkStatusId")]
        public WorkStatus WorkStatus { get; set; }

        public virtual List<Task> Tasks { get; set; }
    }
}
