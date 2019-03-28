using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.QoutationManagement
{
    /// <summary>
    /// Chi tiết báo giá
    /// </summary>
    [Table("QoutationDetail")]
    public class QoutationDetail : Auditable
    {
        [Key()]
        [MaxLength(450)]
        public String Id { get; set; }
        /// <summary>
        /// Mã báo giá
        /// </summary>
        public int QoutationId { get; set; }
        public String ProductName { get; set; }
        /// <summary>
        /// Mã sản phẩm
        /// </summary>
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
        /// Đơn giá
        /// </summary>
		public decimal UnitPriceSell { get; set; }
		public decimal UnitPriceBuy { get; set; }
        /// <summary>
        /// VAT
        /// </summary>
        public double VATBuy { get; set; }
        public double VATSell { get; set; }
        /// <summary>
        /// Tổng tiền
        /// </summary>
        public double TotalPriceBuy { get; set; }
        public double TotalPriceSell { get; set; }

        /// <summary>
        /// chiết khấu
        /// </summary>
        public string Discount { get; set; }

        /// <summary>
        /// QoutationId: khóa ngoại bảng báo giá
        /// </summary>
        [ForeignKey("QoutationId")]
        public Qoutation Qoutation { get; set; }
        /// <summary>
        /// ProductId: khóa ngoại bảng sản phẩm
        /// </summary>
        [ForeignKey("ProductId")]
        public Product Product { get; set; }




    }
}
