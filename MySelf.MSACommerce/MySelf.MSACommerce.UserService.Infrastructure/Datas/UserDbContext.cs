using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.UserService.Core.Entites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.UserService.Infrastructure.Datas
{
    public class UserDbContext(DbContextOptions<UserDbContext> options):DbContext(options)
    {
        public DbSet<TbUser> TbUsers => Set<TbUser>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
