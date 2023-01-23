using Pokedex.Business.Core.Notifications;
using Pokedex.Business.Entities;
using Pokedex.Business.Repositories;

namespace Pokedex.Business.Services
{
    public class PokedexService : IPokedexService
    {

        private readonly INotifier _notifier;
        private readonly IPokemonRepository _pokemonRepository;

        public PokedexService (IPokemonRepository pokemonRepository, INotifier notifier)
        {
            _notifier = notifier;
            _pokemonRepository = pokemonRepository;
        }

        public async Task<Guid?> AddPokemonAsync(Pokemon pokemon)
        {
            var validation = pokemon.Validate();

            if (validation.IsValid)
            {
                _notifier.Notify(validation);
                return null;
            }

            var registredPokemon = await _pokemonRepository.GetByNameAsync(pokemon.Name);

            if (registredPokemon != null)
            {
                _notifier.Notify("Já existe um pokémon cadastrado anteriormente");
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
                _notifier.Notify(validation);
                return;
            }

            var hasPokemon = await _pokemonRepository.HasPokemonAsync(pokemon.Id);

            if (!hasPokemon)
            {
                _notifier.Notify("Não foi posivel encontrar o pokémon informado.");
                return;
            }

            var registredPokemon = await _pokemonRepository.GetByNameAsync(pokemon.Name);

            if (registredPokemon != null && registredPokemon.Id != pokemon.Id)
            {
                _notifier.Notify("Não é possivel alterar o nome desse pokémon pois já existe um outro cadastro com o mesmo nome");
                return;
            }

            _pokemonRepository.Update(pokemon);
        }

        public async Task DeletePokemonAsync(Guid pokemonId)
        {

            var hasPokemon = await _pokemonRepository.HasPokemonAsync(pokemonId);

            if (!hasPokemon)
            {
                _notifier.Notify("Não foi posivel encontrar o pokémon informado. ");
                return;
            }

            _pokemonRepository.Delete(pokemonId);
        }

    }
}
