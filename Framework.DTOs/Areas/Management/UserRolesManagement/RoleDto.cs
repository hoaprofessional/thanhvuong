using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DTOs.Areas.Management.UserRolesManagement
{
    public class RoleDto : IRef<IdentityRole>
    {
        public string Name { get; set; }
    }
}
