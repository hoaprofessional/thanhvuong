using Framework.Models.UserManagement;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.QoutationManagement
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    [Table("Staff")]
    public class Staff : Auditable
    {
		[Key()]
        [MaxLength(450)]
		public String Id { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
		[MaxLength(200)]
		public String Name { get; set; }
        /// <summary>
        /// Nghề nghiệp
        /// </summary>
		[MaxLength(200)]
		public String Career { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
		[MaxLength(2000)]
		public String Address { get; set; }
        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
		[MaxLength(450)]
		public String IdentityCard { get; set; }
        /// <summary>
        /// Mã user liên kết
        /// </summary>
		[MaxLength(450)]
		public String UserId { get; set; }
        /// <summary>
        /// UserId: khóa ngoại bảng user
        /// </summary>
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

	}
}
