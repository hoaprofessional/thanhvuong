using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Models.Utils
{
    /// <summary>
    /// Bảng lưu cache
    /// </summary>
    public class CacheData : Auditable
    {
        /// <summary>
        /// Tên cache
        /// </summary>
        [Key]
        public String Key { get; set; }
        /// <summary>
        /// Giá trị cache
        /// </summary>
        public String Value { get; set; }
        /// <summary>
        /// Ngày hết hạn
        /// </summary>
        public DateTime ExpiredDate { get; set; }
        /// <summary>
        /// Đã hết hạn hay chưa
        /// </summary>
        public bool Expired { get; set; }
    }
}
