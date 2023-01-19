using Pokedex.Business.Entities;
using Pokedex.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Services
{
    public class PokedexService : IPokedexService
    {

        private readonly IPokemonRepository _pokemonRepository;

        public PokedexService (IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<Guid?> AddPokemonAsync(Pokemon pokemon)
        {
            var validation = pokemon.Validate();

            if (validation.IsValid)
            {
                //notificar erro pro usuario
                return null;
            }

            var registredPokemon = await _pokemonRepository.GetByNameAsync(pokemon.Name);

            if (registredPokemon != null)
            {
                //Notificar a inconsistencia para o usuario
                return null;
            }

            await _pokemonRepository.AddAsync(pokemon);

            return pokemon.Id;
        }

        public Task UpdatePokemonAsync(Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

        public Task DeletePokemonAsync(Guid pokemonId)
        {
            throw new NotImplementedException();
        }

    }
}
