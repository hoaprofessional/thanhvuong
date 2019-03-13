using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFramework.Areas.Management.Models
{
    public class RolesViewModel
    {
        public List<IdentityRole> Roles { get; set; }
    }
}
