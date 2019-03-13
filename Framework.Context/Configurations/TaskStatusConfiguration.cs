using Framework.Models.TaskManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Context.Configurations
{
    class TaskStatusConfiguration : IEntityTypeConfiguration<TaskStatus>
    {
        public void Configure(EntityTypeBuilder<TaskStatus> builder)
        {
            builder.ToTable("TaskStatus", Resource1.TaskNamespace);
        }
    }
}
