using Pokedex.Business.Core.Pagination;
using Pokedex.Business.Entities;
using Pokedex.Business.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Services
{
    public interface IPokedexService
    {
        Task<Guid?> AddPokemonAsync(Pokemon pokemon);

        Task UpdatePokemonAsync(Pokemon pokemon);

        Task DeletePokemonAsync(Guid pokemonId);

        Task<Pokemon?> GetByIdAsync(Guid pokemonId);

        Task<PagedList<Pokemon>> Find(FindPokemonQuery query);

    }
}
