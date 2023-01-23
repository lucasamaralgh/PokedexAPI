using Pokedex.Business.Entities;
using Pokedex.Business.Repositories;

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

        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
            var validation = pokemon.Validate();

            if (validation.IsValid)
            {
                //notificar erro pro usuario
                return;
            }

            var hasPokemon = await _pokemonRepository.HasPokemonAsync(pokemon.Id);

            if (!hasPokemon)
            {
                //notificar erro pro usuario
                return;
            }

            var registredPokemon = await _pokemonRepository.GetByNameAsync(pokemon.Name);

            if (registredPokemon != null && registredPokemon.Id != pokemon.Id)
            {
                //Notificar a inconsistencia para o usuario
                return;
            }

            _pokemonRepository.Update(pokemon);
        }

        public async Task DeletePokemonAsync(Guid pokemonId)
        {

            var hasPokemon = await _pokemonRepository.HasPokemonAsync(pokemonId);

            if (!hasPokemon)
            {
                //notificar erro pro usuario
                return;
            }

            _pokemonRepository.Delete(pokemonId);
        }

    }
}
