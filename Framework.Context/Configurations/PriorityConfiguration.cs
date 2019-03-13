using Framework.Models.TaskManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Context.Configurations
{
    /// <summary>
    /// Cấu hình bảng task
    /// </summary>
    class PriorityConfiguration : IEntityTypeConfiguration<Priority>
    {
        public void Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.ToTable("Priority", Resource1.TaskNamespace);
        }
    }
}
