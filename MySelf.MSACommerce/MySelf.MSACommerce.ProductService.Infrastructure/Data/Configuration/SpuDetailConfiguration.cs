﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.ProductService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.ProductService.Infrastructure.Data.Configuration
{
    public class SpuDetailConfiguration : IEntityTypeConfiguration<SpuDetail>
    {
        public void Configure(EntityTypeBuilder<SpuDetail> builder)
        {
            builder.ToTable("tb_spu_detail");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("bigint(20)");

            builder.Property(e => e.Introduction)
                .HasColumnName("introduction")
                .HasColumnType("text")
                .HasComment("商品介绍");

            builder.Property(e => e.Parameter)
                .IsRequired()
                .HasColumnName("parameter")
                .HasColumnType("varchar(2048)")
                .HasComment("商品参数");

            builder.Property(e => e.Spec)
                .IsRequired()
                .HasColumnName("spec")
                .HasColumnType("varchar(2048)")
                .HasComment("商品规格");


        }
    }
}
