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
        Task<Guid?> AddPokemon(Pokemon pokemon);

        Task UpdatePokemon(Pokemon pokemon);

        Task DeletePokemon(Guid pokemonId);


    }
}
