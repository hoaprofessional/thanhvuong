using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// Chức năng liên quan :
/// + CM.T.2.1.6.2 : Xem task
/// + CM.T.2.1.6.3 : Tạo task
/// + CM.T.2.1.6.4 : cập nhật task
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
    /// Trạng thái công việc
    /// </summary>
    [Table("WorkStatus")]
    public class WorkStatus : Auditable
    {
        /// <summary>
        /// Mã trạng thái
        /// </summary>
		[Key()]
        [MaxLength(450)]
		public string Id { get; set; }
        /// <summary>
        /// Mô tả trạng thái
        /// </summary>
        [MaxLength(200)]
		public string Decription { get; set; }

	}
}
