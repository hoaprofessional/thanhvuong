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
    class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Task", Resource1.TaskNamespace);
        }
    }
}
