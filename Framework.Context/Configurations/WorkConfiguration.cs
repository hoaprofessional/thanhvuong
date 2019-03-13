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
    class WorkConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.ToTable("Work", Resource1.TaskNamespace);
        }
    }
}
