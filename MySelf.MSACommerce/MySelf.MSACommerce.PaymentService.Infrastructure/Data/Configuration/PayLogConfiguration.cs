using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySelf.MSACommerce.PaymentService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.PaymentService.Infrastructure.Data.Configuration
{
    public class PayLogConfiguration : IEntityTypeConfiguration<PayLog>
    {
        public void Configure(EntityTypeBuilder<PayLog> builder)
        {
            builder.ToTable("tb_pay_log");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("bigint(20)");

            builder.Property(e => e.OrderId)
                .HasColumnName("order_id")
                .HasColumnType("bigint(20)")
                .HasComment("订单号");

            builder.Property(e => e.PayTime)
                .HasColumnName("pay_time")
                .HasColumnType("datetime")
                .HasComment("支付时间");

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .HasComment("交易状态，1 未支付, 2已支付, 3 已退款, 4 支付错误, 5 已关闭");

            builder.Property(e => e.TotalFee)
                .HasColumnName("total_fee")
                .HasColumnType("bigint(20)")
                .HasComment("支付金额（分）");

            builder.Property(e => e.UserId)
                .HasColumnName("user_id")
                .HasColumnType("bigint(20)")
                .HasComment("用户ID");
        }
    }
}
