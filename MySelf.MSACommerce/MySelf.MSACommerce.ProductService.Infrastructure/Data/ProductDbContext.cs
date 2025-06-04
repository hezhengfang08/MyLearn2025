using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.ProductService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.ProductService.Infrastructure.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Spu> Spus => Set<Spu>();
        public DbSet<SpuDetail> SpuDetails => Set<SpuDetail>();
        public DbSet<Sku> Skus => Set<Sku>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}
