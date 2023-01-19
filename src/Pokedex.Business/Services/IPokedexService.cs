using Pokedex.Business.Entities;
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


    }
}
