using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utils
{
    public class OrderStatusIdHelper
    {
        /// <summary>
        /// Trạng thái đơn đặt hàng : vừa được tạo
        /// </summary>
        public static readonly string AlreadyCreated = "already_created";

        /// <summary>
        /// Giám đốc phê duyệt đơn hàng
        /// </summary>
        public static readonly string ConfirmOrder = "confirm_order";
        public static readonly string RejectOrder = "reject_order";

        /// <summary>
        /// Trạng thái đơn đặt hàng: Đã phê duyệt : Trưởng phòng sale
        /// </summary>
        public static readonly string SalesManagerApprove = "manager_approve_order";
        /// <summary>
        /// Trạng thái đơn đặt hàng: Từ chối : Trưởng phòng sale
        /// </summary>
        public static readonly string SalesManagerReject = "manager_reject_order";

        /// <summary>
        /// Trạng thái đơn đặt hàng: Đã đặt hàng
        /// </summary>
        public static readonly string AccountantHasOrdered = "accountant_has_ordered";

        /// <summary>
        /// Trạng thái đơn đặt hàng : Hàng đang trên đường về
        /// </summary>
        public static readonly string GoodOnWay = "good_on_way";

        /// <summary>
        /// Trạng thái đơn đặt hàng : Hàng sẵn sàng giao
        /// </summary>
        public static readonly string ReadyToDeliver = "ready_to_deliver";
        /// <summary>
        /// Đề nghị giao hàng
        /// </summary>
        public static readonly string RecommendedDelivery = "recommended_delivery";
        /// <summary>
        /// Trạng thái đơn đặt hàng : Phê duyệt : Trưởng phòng kỹ thuật
        /// </summary>
        public static readonly string ChiefTechnicalApprove = "chief_technical_approve";

        /// <summary>
        /// Trạng thái đơn đặt hàng : Đã giao hàng
        /// </summary>
        public static readonly string ChiefTechnicalDeliver = "chief_technical_deliver";

        /// <summary>
        /// Trạng thái đơn đặt hàng : Đã thu tiền
        /// </summary>
        public static readonly string AccountantReceiveMoney = "accountant_receive_money";
        /// <summary>
        /// Trạng thái đơn đặt hàng : Khách hàng nợ
        /// </summary>
        public static readonly string ClientDept = "client_dept";
        /// <summary>
        /// Trưởng phòng kế toán duyệt đặt hàng
        /// </summary>
        public static readonly string AccountingManagerApprove = "accountanting_manager_approve_ordered";
        public static readonly string AccountingManagerReject = "accountanting_manager_reject_ordered";



    }
}
