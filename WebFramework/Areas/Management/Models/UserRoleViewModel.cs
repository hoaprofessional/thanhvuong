using Framework.DTOs.Areas.Management.UserRolesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFramework.Areas.Management.Models
{
    public class UserRoleViewModel
    {
        public List<UserDto> Users { get; set; }
        public List<RoleDto> Roles { get; set; }
    }
}
