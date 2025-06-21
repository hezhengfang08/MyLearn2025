using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.SeckillService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SeckillService.Infrastructure.Data
{
    public class SecKillDbContext(DbContextOptions<SecKillDbContext> options) : DbContext(options)
    {
        public DbSet<SecKillProduct> SecKillProducts => Set<SecKillProduct>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
