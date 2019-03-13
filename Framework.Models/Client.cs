using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.QoutationManagement
{
    /// <summary>
    /// Khách hàng
    /// </summary>
    [Table("Client")]
    public class Client : Auditable
    {
        /// <summary>
        /// Id khách hàng
        /// </summary>
		[Key()]
        [MaxLength(450)]
		public String Id { get; set; }
        /// <summary>
        /// Tên khách hàng
        /// </summary>
		[MaxLength(200)]
		public String Name { get; set; }
        /// <summary>
        /// Địa chỉ khách hàng
        /// </summary>
		[MaxLength(5000)]
		public String Address { get; set; }
        /// <summary>
        /// Số điện thoại khách hàng
        /// </summary>
		[MaxLength(450)]
		public String PhoneNumber { get; set; }
        /// <summary>
        /// Email khách hàng
        /// </summary>
		[MaxLength(1000)]
		public String Email { get; set; }

	}
}
