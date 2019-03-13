using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Models.QoutationManagement
{
    [Table("OrderDetail")]
    public class OrderDetail : Auditable
    {
        [Key()]
        [MaxLength(450)]
        public String Id { get; set; }
        [MaxLength(450)]
        public String OrderId { get; set; }
        [MaxLength(450)]
        public String ProductId { get; set; }
        public String ProductNote { get; set; }
        public String ProductDetail { get; set; }
        public String ProductUnit { get; set; }
        public String ProductSize { get; set; }
        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
		public int ProductQuantity { get; set; }
        /// <summary>
        /// Đơn giá bán
        /// </summary>
		public decimal UnitPrice { get; set; }
        /// <summary>
        /// VAT
        /// </summary>
        public double VAT { get; set; }
        /// <summary>
        /// Tổng tiền
        /// </summary>
        public double TotalPrice { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
