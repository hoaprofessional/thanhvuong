using Framework.Models.UserManagement;
using Framework.Utils.Anotations.DtoAnotation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.Areas.Management.UserRolesManagement
{
    public class UserRolesDto : IRef<ApplicationUser>,IRef<IdentityRole>
    {

        [MapColumn("ApplicationUserId")]
        public string UserId { get; set; }

        [MapColumn("ApplicationUserName")]
        public string UserName { get; set; }

        [MapColumn("IdentityUserRoleRoleId")]
        public string RoleId { get; set; }

        [MapColumn("IdentityRoleName")]
        public string RoleName { get; set; }
    }
}
