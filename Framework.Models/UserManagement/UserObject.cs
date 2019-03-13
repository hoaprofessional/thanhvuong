using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Framework.Models.UserManagement
{
    public class UserObject
    {
        [Key]
        [MaxLength(450)]
        public String Id { get; set; }
        [MaxLength(100)]
        public String Name { get; set; }
        [MaxLength(450)]
        public String RoleId { get; set; }
        [ForeignKey("RoleId")]
        public IdentityRole Role { get; set; }
        public String DefaultCarrer { get; set; }
    }
}
