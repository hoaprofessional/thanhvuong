using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Models.Utils
{
    /// <summary>
    /// Bảng logger
    /// </summary>
    public class Logger : Auditable
    {
        [Key]
        public String Id { get; set; }
        /// <summary>
        /// Nội dung logger
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// Danger
        /// Infomation
        /// Warning
        /// </summary>
        public String LogType { get; set; }
    }
}
