using Framework.Models.QoutationManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Context.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
           .Property(p => p.RemainingPrice)
            .HasComputedColumnSql("[TotalPrice] - [PaidPrice]");
        }
    }
}
