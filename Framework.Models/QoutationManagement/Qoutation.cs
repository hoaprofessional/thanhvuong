using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.QoutationManagement
{
    /// <summary>
    /// Báo giá
    /// </summary>
    [Table("Qoutation")]
    public class Qoutation : Auditable
    {
        /// <summary>
        /// Số báo giá
        /// </summary>
		public int Id { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
		[MaxLength(450)]
		public String ClientId { get; set; }
        /// <summary>
        /// Mã người báo giá
        /// </summary>
		[MaxLength(450)]
		public String QouteStaffId { get; set; }
        /// <summary>
        /// Mã người xác nhận
        /// </summary>
		[MaxLength(450)]
		public String ConfirmStaffId { get; set; }
        /// <summary>
        /// Mã giám đốc dự án
        /// </summary>
		[MaxLength(450)]
		public String ManagerId { get; set; }
        /// <summary>
        /// Sales Admin
        /// </summary>
        public String SalesAdminId { get; set; }
        /// <summary>
        /// Tổng tiền báo giá nhập
        /// </summary>
        public double TotalPriceBuy { get; set; }
        /// <summary>
        /// Tổng tiền báo giá bán
        /// </summary>
        public double TotalPriceSell { get; set; }
        /// <summary>
        /// Thời gian ước tính tính bằng ngày
        /// </summary>
		public decimal EstimatedInstallationTime { get; set; }
        /// <summary>
        /// Địa điểm giao hàng
        /// </summary>
        [MaxLength(1000)]
        public string DeliveryPlace { get; set; }
        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        public String PaymentMethod { get; set; }
        public String QoutationStatusId { get; set; }
        /// <summary>
        /// ClientId: khóa ngoại bảng khách hàng
        /// </summary>
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        /// <summary>
        /// QouteStaffId: khóa ngoại bảng nhân viên
        /// </summary>
        [ForeignKey("QouteStaffId")]
        public virtual Staff QouteStaff { get; set; }
        /// <summary>
        /// ConfirmStaffId: khóa ngoại bảng nhân viên
        /// </summary>
        [ForeignKey("ConfirmStaffId")]
        public virtual Staff ConfirmStaff { get; set; }
        /// <summary>
        /// ConfirmStaffId: khóa ngoại bảng nhân viên
        /// </summary>
        [ForeignKey("SalesAdminId")]
        public virtual Staff SalesAdmin { get; set; }
        /// <summary>
        /// ManageId: khóa ngoại bảng nhân viên
        /// </summary>
        [ForeignKey("ManagerId")]
        public virtual Staff Manager { get; set; }

        [ForeignKey("QoutationStatusId")]
        public virtual QoutationStatus QoutationStatus { get; set; }


        public virtual List<QoutationDetail> QoutationDetails { get; set; }
        public string RejectReason { get; set; }
    }
}
