using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Models.QoutationManagement
{
    [Table("OrderStatus")]
    public class OrderStatus : Auditable
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
