using Framework.Models.UserManagement;
using Framework.Utils.Anotations.DtoAnotation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.Utils.PermissionServiceDto
{
    public class UserPermissionDto : IRef<ApplicationUser>, 
        IRef<IdentityUserRole<string>>,
        IRef<Permission>, IRef<IdentityRole>
    {
        [MapColumn("ApplicationUserId")]
        public String UserId { get; set; }
        [MapColumn("IdentityUserRoleRoleId")]
        public String RoleId { get; set; }
        public String PermissionName { get; set; }
    }
}
