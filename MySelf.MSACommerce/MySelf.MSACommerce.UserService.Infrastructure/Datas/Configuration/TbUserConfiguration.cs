using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySelf.MSACommerce.UserService.Core;
using MySelf.MSACommerce.UserService.Core.Entites;


namespace MySelf.MSACommerce.UserService.Infrastructure.Datas.Configuration
{
    public class TbUserConfiguration : IEntityTypeConfiguration<TbUser>
    {
        public void Configure(EntityTypeBuilder<TbUser> builder)
        {
            builder.ToTable("tb_user");
            builder.Property(e => e.Id)
            .HasColumnName("id");
            builder.HasIndex(e => e.UserName)
           .IsUnique();
            builder.Property(e => e.UserName)
           .IsRequired()
           .HasColumnName("username")
           .HasMaxLength(DataSchemaConstants.DefaultUsernameMaxLength)
           .HasComment("用户名");
            builder.Property(e => e.Password)
           .IsRequired()
           .HasColumnName("password")
           .HasMaxLength(DataSchemaConstants.DefaultPasswordMaxLength)
           .HasComment("密码，加密存储");
            builder.Property(e => e.Phone)
            .HasColumnName("phone")
            .HasMaxLength(DataSchemaConstants.DefaultPhoneLength)
            .HasComment("注册手机号");
            builder.Property(e => e.Salt)
           .IsRequired()
           .HasColumnName("salt")
           .HasMaxLength(DataSchemaConstants.DefaultSaltMaxLength)
           .HasComment("密码加密的salt值");













































        }
    }
}
