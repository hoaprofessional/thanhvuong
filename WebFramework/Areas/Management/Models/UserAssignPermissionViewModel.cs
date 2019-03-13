using System.Collections.Generic;
using WebCore.Services.Share.Admins.Users.Dto;
using WebCore.Services.Share.Commons.Permissions.Dto;

namespace WebFramework.Areas.Management.Models
{
    public class UserAssignPermissionViewModel
    {
        public PermissionDto TreeViewPermission { get; set; }
        public List<RoleDto> AllRoles { get; set; }
    }
}
