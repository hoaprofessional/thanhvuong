using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.QoutationManagement
{
    /// <summary>
    /// Sản phẩm
    /// </summary>
    [Table("Product")]
    public class Product : Auditable
    {
		[Key()]
        [MaxLength(450)]
		public String Id { get; set; }
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
		[MaxLength(200)]
		public String Name { get; set; }
        /// <summary>
        /// Mô tả sản phẩm
        /// </summary>
		public String Decription { get; set; }
        /// <summary>
        /// Kích thước sản phẩm
        /// </summary>
		public String Size { get; set; }
        /// <summary>
        /// Đơn vị tính
        /// </summary>
		[MaxLength(450)]
		public String Unit { get; set; }
        /// <summary>
        /// Đơn giá sản phẩm
        /// </summary>
		public decimal Price { get; set; }
        /// <summary>
        /// Hình ảnh sản phẩm
        /// Nhiều hình ví dụ (hinh1.png,hinh2.png)
        /// </summary>
		[MaxLength(2000)]
		public String Images { get; set; }

	}
}
