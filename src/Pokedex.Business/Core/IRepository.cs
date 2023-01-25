using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Core
{
    public interface IRepository
    {
        Task CommitAsync();
    }
}
