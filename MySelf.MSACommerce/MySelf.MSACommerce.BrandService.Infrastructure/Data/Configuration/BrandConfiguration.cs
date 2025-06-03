using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySelf.MSACommerce.BrandService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.BrandService.Infrastructure.Data.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("tb_brand");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("bigint(20)")
                .HasComment("品牌id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(32)")
                .HasComment("品牌名称");

            builder.Property(e => e.Image)
                .HasColumnName("image")
                .HasColumnType("varchar(128)")
                .HasComment("品牌图片地址");

            builder.Property(e => e.Letter)
                .IsRequired()
                .HasColumnName("letter")
                .HasColumnType("char(1)")
                .HasDefaultValueSql("''")
                .HasComment("品牌的首字母");
        }
    }
}
