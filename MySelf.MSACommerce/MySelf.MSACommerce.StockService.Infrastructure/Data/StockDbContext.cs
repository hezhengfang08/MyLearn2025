using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.StockService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.StockService.Infrastructure.Data
{
    public class StockDbContext(DbContextOptions<StockDbContext> options):DbContext(options)
    {
        public DbSet<SkuStock> SkuStocks => Set<SkuStock>();
        public DbSet<StockResv> StockResvs => Set<StockResv>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
