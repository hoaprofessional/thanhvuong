using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Framework.Models.UserManagement;

namespace Framework.Context.Configurations
{
    /// <summary>
    /// Cấu hình bảng Permission
    /// </summary>
    class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
        }
    }
}
