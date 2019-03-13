using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.QoutationManagementDto.BaseOrderDto
{
    public class OrderDto :
        IRef<Order>,
        IRef<Client>,
        IRef<Staff>,
        IRef<QoutationStatus>
    {
        public int Id { get; set; }
        public bool CanEdit { get; set; }
        public string OrderId { get; set; }
        public int QoutationId { get; set; }
        public string OrderCreationStaffId { get; set; }
        //Người tạo
        public string StaffName { get; set; }
        //Mã khách hàng
        public string QoutationClientId { get; set; }
        //Khách hàng
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        //Ngày tạo
        public DateTime? OrderCreationTime { get; set; }
        public DateTime? OrderModifiedTime { get; set; }
        public DateTime? OrderExpectedReturnGoodDate { get; set; }
        public DateTime? OrderExpectedDeliveryDate { get; set; }
        public String OrderOrderStatusId { get; set; }
        //trạng thái báo giá
        public String OrderStatusName { get; set; }
        //tổng tiền
        public double OrderTotalPrice { get; set; }
        public String OrderNote { get; set; }
    }
}
