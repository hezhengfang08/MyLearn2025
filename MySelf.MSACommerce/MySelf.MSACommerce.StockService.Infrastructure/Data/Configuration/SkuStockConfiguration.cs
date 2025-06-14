using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySelf.MSACommerce.StockService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.StockService.Infrastructure.Data.Configuration
{
    public class SkuStockConfiguration : IEntityTypeConfiguration<SkuStock>
    {
        public void Configure(EntityTypeBuilder<SkuStock> builder)
        {
            builder.ToTable("tb_sku_stock");
            builder.Property(e=>e.Id)
                .HasColumnName("id")
                .HasColumnType("bigint(20)")
                .HasComment("商品SKU Id");

            builder.Property(e => e.TotalQty)
            .HasColumnName("total_qty")
            .HasColumnType("bigint(20)")
            .HasComment("库存总数量");

            builder.Property(e => e.AvailQty)
           .HasColumnName("avail_qty")
           .HasColumnType("bigint(20)")
           .HasComment("可用数量");

            builder.Property(e => e.ResvQty)
            .HasColumnName("resv_qty")
            .HasColumnType("bigint(20)")
            .HasComment("预留数量");

            builder.HasMany(s => s.StockResvs)
            .WithOne(s => s.SkuStock)
            .HasForeignKey(s => s.SkuId);
        }
    }
}
