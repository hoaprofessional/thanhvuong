using Framework.Models.QoutationManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Context.Configurations
{
    public class QoutationDetailConfiguration : IEntityTypeConfiguration<QoutationDetail>
    {
        public void Configure(EntityTypeBuilder<QoutationDetail> builder)
        {
            builder
           .Property(p => p.TotalPriceBuy)
            .HasComputedColumnSql(String.Format("({0}*{1} + {1})*{2}",
            nameof(QoutationDetail.VATBuy), nameof(QoutationDetail.UnitPriceBuy), nameof(QoutationDetail.ProductQuantity)));
            builder
            .Property(p => p.TotalPriceSell)
           .HasComputedColumnSql(String.Format("({0}*{1} + {1})*{2}",
            nameof(QoutationDetail.VATSell), nameof(QoutationDetail.UnitPriceSell), nameof(QoutationDetail.ProductQuantity)));
        }
    }
}
