using Framework.Models.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.Areas.Management.UserRolesManagement
{
    public class UserDto : IRef<ApplicationUser>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<String> Roles { get; set; }
    }
}
