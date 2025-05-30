using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MySelf.MSACommerce.SharedKernel.Domain;
using MySelf.MSACommerce.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.Infrastructure.EntityFrameworkCore.Interceptors
{
    public class AuditEntityInterceptor(IUser currentUser) :SaveChangesInterceptor
    {
        public override  InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result )
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result );
        }
        private void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext == null)
            {
                return; 
            }
            foreach(var entity in dbContext.ChangeTracker.Entries<BaseAuditEntity>())
            {
                if (entity.State is not (EntityState.Added or EntityState.Modified)) continue;
                var now = DateTimeOffset.Now;
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreateAt = now;
                    entity.Entity.LastModifiedAt = now;
                }
                else
                {
                    entity.Entity.LastModifiedAt = now;
                }
            }
            foreach (var entry in dbContext.ChangeTracker.Entries<AuditWithUserEntity>())
            {
                if (entry.State is not (EntityState.Added or EntityState.Modified)) continue;

                if (currentUser.Id is null) continue;

                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedBy = currentUser.Id;
                else
                    entry.Entity.LastModifiedBy = currentUser.Id;
            }
        }
    }
}
