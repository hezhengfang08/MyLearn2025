using Microsoft.EntityFrameworkCore;
using MySelf.MSACommerce.CategoryService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CategoryService.Infrastructure.Data
{
    public class CategoryDbContext(DbContextOptions<CategoryDbContext> options):DbContext(options)
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<CategoryBrands> categoryBrands => Set<CategoryBrands>();
        public DbSet<SpecKey> SpecKeys => Set<SpecKey>();
        public DbSet<ParameterGroup> ParameterGroups => Set<ParameterGroup>();
        public DbSet<ParameterKey> ParameterKeys => Set<ParameterKey>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
