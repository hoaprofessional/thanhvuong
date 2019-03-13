using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Models.QoutationManagement
{
    [Table("Order")]
    public class Order : Auditable
    {
        [MaxLength(450)]
        public String Id { get; set; }
        public String OrderStatusId { get; set; }
        public String ClientId { get; set; }
        public string CreationStaffId { get; set; }
        public int? QoutationId { get; set; }
        public decimal EstimatedInstallationTime { get; set; }
        public decimal Promotion { get; set; }
        public bool? StopUpdateExpectedReturnGoodDate { get; set; }
        public double TotalPrice { get; set; }
        public double PaidPrice { get; set; }
        public double RemainingPrice { get; set; }
        [ForeignKey("QoutationId")]
        public virtual Qoutation Qoutation { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus OrderStatus { get; set; }
        public Staff CreationStaff { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        /// <summary>
        /// Ngày dự kiến hàng về
        /// </summary>
        public DateTime? ExpectedReturnGoodDate { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
