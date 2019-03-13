using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.PrintPdfDto
{
    public class QoutationDto : IRef<Qoutation>
    {
        /// <summary>
        /// Mã báo giá
        /// </summary>
        public String Id { get; set; }
        /// <summary>
        /// Tên công ty
        /// </summary>
        public String CompanyName { get; set; }
        /// <summary>
        /// Trụ sở
        /// </summary>
        public String Headquarters { get; set; }
        /// <summary>
        /// Liên hệ
        /// </summary>
        public String Contact { get; set; }
        /// <summary>
        /// Web
        /// </summary>
        public String WebsiteName { get; set; }
        /// <summary>
        /// Hotline
        /// </summary>
        public String HotLine { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public String ClientName { get; set; }
        /// <summary>
        /// Địa chỉ khách hàng
        /// </summary>
        public String ClientAddress { get; set; }
        /// <summary>
        /// SĐT Khách hàng
        /// </summary>
        public String ClientPhoneNumber { get; set; }
        /// <summary>
        /// Email Khách hàng
        /// </summary>
        public String ClientEmail { get; set; }
        /// <summary>
        /// Số thư báo giá
        /// </summary>
        public int LetterOfQuotationNumber { get; set; }
        /// <summary>
        /// Tên người báo giá
        /// </summary>
        public String QouteStaffName { get; set; }
        /// <summary>
        /// Tên người xác nhận
        /// </summary>
        public String ConfirmStaff { get; set; }
        /// <summary>
        /// Tên sales admin
        /// </summary>
        public String SalesAdminName { get; set; }
        /// <summary>
        /// Tên giám đốc dự án
        /// </summary>
        public String ManagerName { get; set; }
        /// <summary>
        /// Tổng tiền hóa đơn
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// Chi tiết hóa đơn
        /// </summary>
        public List<QoutationDetailDto> QoutationDetails { get; set; }
        /// <summary>
        /// Thời điểm tạo
        /// </summary>
        public DateTime? CreationTime { get; set; }
        /// <summary>
        /// Thời gian ước tính (tính bằng ngày)
        /// </summary>
        public decimal EstimatedInstallationTime { get; set; }
        /// <summary>
        /// Địa điểm giao hàng
        /// </summary>
        public string DeliveryPlace { get; set; }
        /// <summary>
        /// Phương thức thanh toán
        /// </summary>
        public string PaymentMethod { get; set; }
    }
}
