using Framework.Models.QoutationManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.PrintPdfDto
{
    public class QoutationDetailDto : IRef<QoutationDetail>
    {
        /// <summary>
        /// Mã báo giá
        /// </summary>
        public int QoutationId { get; set; }
        /// <summary>
        /// Mã sản phẩm
        /// </summary>
        public String ProductName { get; set; }
        /// <summary>
        /// Hình ảnh sản phẩm
        /// </summary>
        public String ProductImage { get; set; }
        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
		public int ProductQuantity { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
		public decimal UnitPrice { get; set; }
        /// <summary>
        /// Tổng tiền
        /// </summary>
		public decimal TotalPrice { get; set; }
        /// <summary>
        /// Đơn vị tính
        /// </summary>
        public String Unit { get; set; }
        /// <summary>
        /// Kích thước
        /// </summary>
        public String Size { get; set; }
        public string Discount { get; set; }
        /// <summary>
        /// Chiết khấu
        /// </summary>
        public string ManageId { get; set; }

        public String SizeDisplay
        {
            get
            {
                return Size.Replace(" ", "").Replace("x"," x ");
            }
        }


        /// <summary>
        /// Mô tả
        /// </summary>
        public String Decription { get; set; }
    }
}
