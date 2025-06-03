using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.BrandService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.BrandService.Infrastructure.Data
{
    public class BrandDbContext(DbContextOptions<BrandDbContext> options):DbContext(options)
    {
        public DbSet<Brand> Brands => Set<Brand>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
