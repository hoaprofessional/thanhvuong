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
    /// Cấu hình bảng
    /// </summary>
    class WorkStatusConfiguration : IEntityTypeConfiguration<WorkStatus>
    {
        public void Configure(EntityTypeBuilder<WorkStatus> builder)
        {
            builder.ToTable("WorkStatus", Resource1.TaskNamespace);
        }
    }
}
