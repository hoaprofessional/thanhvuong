using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utils
{
    public class QoutationStatusIdHelper
    {
        /// <summary>
        /// Trạng thái báo giá : vừa được tạo
        /// </summary>
        public static readonly string AlreadyCreated = "already_created";

        /// <summary>
        /// Trạng thái báo giá: Trưởng phòng sale đã phê duyệt báo giá từ nhân viên sale
        /// </summary>
        public static readonly string SalesManagerApproveSalesStaff = "manager_approve_Qoutation";

        /// <summary>
        /// Trạng thái báo giá: Nhân viên kế toán đã điền giá và đang đợi phê duyệt
        /// </summary>
        public static readonly string AccountantFilledPriceBuy = "accountant_fill_buy";
        public static readonly string AccountantFilledPriceSell = "accountant_fill_sell";
        /// <summary>
        /// Trạng thái báo giá: nhân viên kế toán bán từ chối về giá mua
        /// </summary>
        public static readonly string AccountantRejectSellPrice = "accountant_reject_sell_price";

        /// <summary>
        /// Trạng thái báo giá : Trưởng phòng kế toán đã phê duyệt giá của báo giá
        /// </summary>
        public static readonly string AccountingManagerApprovedPrice = "accounting_manager_approve_price";

        /// <summary>
        /// Trạng thái báo giá : Trưởng phòng kế toán không phê duyệt giá của báo giá
        /// </summary>
        public static readonly string AccountingManagerRejectApprovedPrice = "accounting_manager_reject_price";

        /// <summary>
        /// Trạng thái báo giá : Trưởng phòng sale đã phê duyệt báo giá từ phòng kế toán
        /// </summary>
        public static readonly string SalesManagerApproveAccountingDepartment = "sales_manager_approve_price_qoutation";

        /// <summary>
        /// Trạng thái báo giá : Trưởng phòng sale không phê duyệt báo giá từ phòng kế toán
        /// </summary>
        public static readonly string SalesManagerRejectPriceAccountingDepartment = "sales_manager_reject_price_qoutation";

        /// <summary>
        /// Trạng thái báo giá : Đang báo giá
        /// </summary>
        public static readonly string Quoting = "qouting";

        /// <summary>
        /// Trạng thái báo giá : Khách hàng đã đồng ý báo giá
        /// </summary>
        public static readonly string ClientAccepted = "client_accept";

        /// <summary>
        /// Trạng thái báo giá : Khách hàng không đồng ý báo giá
        /// </summary>
        public static readonly string ClientRejected = "client_reject";

        /// <summary>
        /// Trạng thái báo giá : Đã hủy
        /// Nếu salesManagerId=null thì báo giá bị hủy do hệ thống
        /// </summary>
        public static readonly string Terminated = "terminated";

    }
}
