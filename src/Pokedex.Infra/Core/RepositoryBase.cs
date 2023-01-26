using Pokedex.Business.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Infra.Core
{
    public class RepositoryBase : IRepository
    {
        protected readonly EFDbContext _efContext;

        public RepositoryBase(EFDbContext efContext)
        {
            _efContext = efContext;
        }

        public Task CommitAsync()
        {
            return _efContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _efContext?.Dispose();
        }
    }
}