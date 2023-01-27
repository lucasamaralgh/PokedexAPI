using Pokedex.Business.Core;
using Pokedex.Business.Core.Pagination;
using Pokedex.Business.Entities;
using Pokedex.Business.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Repositories
{
    public interface IPokemonRepository : IRepository
    {
        Task AddAsync(Pokemon pokemon);

        void Update(Pokemon pokemon);

        void Delete(Guid pokemonId);

        Task<bool> HasPokemonAsync(Guid pokemonID);

        Task<Pokemon?> GetByIdAsync(Guid pokemonID);

        Task<Pokemon?> GetByNameAsync(string name);

        Task<PagedList<Pokemon>> FindAsync(FindPokemonQuery query);
    }
}
