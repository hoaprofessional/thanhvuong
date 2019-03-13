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
/// + CM.T.2.1.1 : Xem công việc
/// + CM.T.2.1.2 : Tìm kiếm công việc
/// + CM.T.2.1.3 : Tạo công việc mới
/// + CM.T.2.1.4 : Cập nhật thông tin công việc
/// + CM.T.2.1.5 : Xóa công việc
/// 
/// Màn hình liên quan :
/// + CM.T.F.4 : Danh sách task của công việc
/// + CM.T.F.5 : Tạo task
/// + CM.T.F.6 : Cập nhật task
/// + CM.T.F.1 : Trang xem công việc
/// + CM.T.F.2 : Trang tạo công việc
/// + CM.T.F.3 : Trang cập nhật công việc
/// 
/// Schema : "Task"
/// 
/// Người tạo : Hùng-DV
/// Ngày tạo  : 2018-07-15
/// Cập nhật lần cuối : chưa có cập nhật
namespace Framework.Models.TaskManagement
{
    /// <summary>
    /// Độ ưu tiên của task, công việc
    /// </summary>
    [Table("Priority")]
    public class Priority : Auditable
    {
        /// <summary>
        /// Mã độ ưu tiên
        /// </summary>
		[Key()]
        [MaxLength(450)]
		public string Id { get; set; }
        /// <summary>
        /// Tên độ ưu tiên
        /// </summary>
		[MaxLength(200)]
		public string Name { get; set; }
        /// <summary>
        /// Giá trị ưu tiên
        /// </summary>
		public int PriorityValue { get; set; }
        /// <summary>
        /// Màu priority
        /// </summary>
        public string Color { get; set; }
	}
}
