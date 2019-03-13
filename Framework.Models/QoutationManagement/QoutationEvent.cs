using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.QoutationManagement
{
    /// <summary>
    /// Quá trình xử lý hóa đơn
    /// Lưu thông tin user để tiện việc lượng hóa thời gian báo giá
    /// </summary>
    [Table("QoutationEvent")]
    public class QoutationEvent : Auditable
    {
		[Key()]
        [MaxLength(450)]
		public String Id { get; set; }
        /// <summary>
        /// Mã báo giá của quá trình này
        /// </summary>
		public int QoutationId { get; set; }
        /// <summary>
        /// Mã nhân viên thực hiện
        /// </summary>
		[MaxLength(450)]
		public String StaffId { get; set; }
        /// <summary>
        /// Mã trạng thái báo giá
        /// Dùng để xem quá trình xử lý hóa đơn này là dẫn đến trạng thái báo giá nào
        /// </summary>
        [MaxLength(450)]
        public String QoutationStatusId { get; set; }
        /// <summary>
        /// QoutationId: khóa ngoại bảng báo giá
        /// </summary>
        [ForeignKey("QoutationId")]
        public Qoutation Qoutation { get; set; }
        /// <summary>
        /// StaffId: khóa ngoại bảng nhân viên
        /// </summary>
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }
        /// <summary>
        /// QoutationStatusId: khóa ngoại bảng trạng thái báo giá
        /// </summary>
        [ForeignKey("QoutationStatusId")]
        public QoutationStatus QoutationStatus { get; set; }
    }
}
