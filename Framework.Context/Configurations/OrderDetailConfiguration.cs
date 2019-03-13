using Framework.Models.QoutationManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Context.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder
           .Property(p => p.TotalPrice)
            .HasComputedColumnSql("([VAT]*[UnitPrice] + [UnitPrice])*[ProductQuantity]");
        }
    }
}
