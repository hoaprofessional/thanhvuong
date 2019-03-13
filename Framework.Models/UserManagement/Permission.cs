using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Models.UserManagement
{
    /// <summary>
    /// Quyền chức năng
    /// </summary>
    [Table("Permission")]
    public class Permission
    {
        [Key()]
        [MaxLength(450)]
        public string Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
