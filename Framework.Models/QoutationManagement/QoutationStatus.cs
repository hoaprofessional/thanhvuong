using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models.QoutationManagement
{
    /// <summary>
    /// Tình trạng báo giá
    /// </summary>
    [Table("QoutationStatus")]
    public class QoutationStatus : Auditable
    {
		[Key()]
        [MaxLength(450)]
		public String Id { get; set; }
        /// <summary>
        /// Mô tả tình trạng báo giá
        /// </summary>
		[MaxLength(2000)]
		public String Name { get; set; }

	}
}
