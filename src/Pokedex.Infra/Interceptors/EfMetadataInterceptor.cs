using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pokedex.Business.Core;
using System.Data;

namespace Pokedex.Infra.Interceptors
{
    public class EfMetadataInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var changeData = DateTime.Now;

            var entries = eventData.Context!.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var entity = entry.Entity as Entity;

                if (entry.State == EntityState.Added)
                    entity!.SetCreationDate(changeData);
                else
                    entry.Property(nameof(Entity.CreatedAt)).IsModified = false;

                entity!.SetUpdateDate(changeData);
            }


            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
