using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Models.QoutationManagement
{
    [Table("OrderEvent")]
    public class OrderEvent : Auditable
    {
        [Key()]
        [MaxLength(450)]
        public String Id { get; set; }
        /// <summary>
        /// Mã đơn đặt hàng của quá trình này
        /// </summary>
        [MaxLength(450)]
        public string OrderId { get; set; }
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
        public String OrderStatusId { get; set; }
        /// <summary>
        /// QoutationId: khóa ngoại bảng báo giá
        /// </summary>
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        /// <summary>
        /// StaffId: khóa ngoại bảng nhân viên
        /// </summary>
        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }
        /// <summary>
        /// QoutationStatusId: khóa ngoại bảng trạng thái báo giá
        /// </summary>
        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }
    }
}
