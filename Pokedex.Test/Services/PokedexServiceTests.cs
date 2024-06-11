using Bogus;
using FluentAssertions;
using Moq;
using Pokedex.Business.Core.Notifications;
using Pokedex.Business.Entities;
using Pokedex.Business.Enums;
using Pokedex.Business.Repositories;
using Pokedex.Business.Services;
using Xunit;
using FluentValidationResult = FluentValidation.Results.ValidationResult;

namespace Pokedex.Tests.Services
{
    public class PokedexServiceTests
    {
        private readonly Mock<IPokemonRepository> _pokemonRepository;
        private readonly Mock<INotifier> _notifier;
        private readonly Faker _faker;

        public PokedexServiceTests()
        {
            _pokemonRepository = new Mock<IPokemonRepository>();
            _notifier = new Mock<INotifier>();
            _faker = new Faker();
        }

        [Fact(DisplayName = "Deve lançar notificações e impedir o cadastro quando o pokemon informado não for válido.")]
        public async Task AddPokemon_WhenPokemonInvalid()
        {
            // Arrange
            var pokemon = new Pokemon(string.Empty, Guid.Empty, _faker.Random.Enum<Gender>(), 1, 1, 1, 1);
            var service = new PokedexService(_pokemonRepository.Object, _notifier.Object);

            // Act
            var result = await service.AddPokemonAsync(pokemon);

            // Asserts
            _notifier.Verify(n => n.Notify(It.IsAny<FluentValidationResult>()), Times.Once);
            _pokemonRepository.Verify(pr => pr.GetByNameAsync(It.IsAny<string>()), Times.Never);

            result.Should().BeNull();
        }

        [Fact(DisplayName = "Deve lançar uma notificação e impedir o cadastro quando já existir um pokémon com o mesmo nome informado.")]
        public async Task AddPokemon_WhenPokemonDuplicated()
        {
            // Arrange
            var pokemon = new Pokemon(Guid.NewGuid().ToString(), Guid.NewGuid(), _faker.Random.Enum<Gender>(), 1, 1, 1, 1);
            var service = new PokedexService(_pokemonRepository.Object, _notifier.Object);

            _pokemonRepository.Setup(pr => pr.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Pokemon(string.Empty, Guid.Empty, _faker.Random.Enum<Gender>(), 1, 1, 1, 1));

            // Act
            var result = await service.AddPokemonAsync(pokemon);

            // Asserts
            _notifier.Verify(n => n.Notify("Já existe um pokémon cadastrado anteriormente"), Times.Once);
            _pokemonRepository.Verify(pr => pr.AddAsync(It.IsAny<Pokemon>()), Times.Never);

            result.Should().BeNull();
        }

        [Fact(DisplayName = "Deve cadastrar o pokémon com sucesso quando ele passar em todas as validações.")]
        public async Task AddPokemon_WhenSuccess()
        {
            // Arrange
            var pokemon = new Pokemon(Guid.NewGuid().ToString(), Guid.NewGuid(), _faker.Random.Enum<Gender>(), 1, 1, 1, 1);
            var service = new PokedexService(_pokemonRepository.Object, _notifier.Object);

            // Act
            var result = await service.AddPokemonAsync(pokemon);

            // Asserts
            _pokemonRepository.Verify(pr => pr.AddAsync(pokemon), Times.Once);

            result.Should().Be(pokemon.Id);
        }

        [Fact(DisplayName = "Deve lançar notificações e impedir o update quando o pokemon informado não for válido.")]
        public async Task UpdatePokemon_WhenPokemonInvalid()
        {
            // Arrange
            var pokemon = new Pokemon(string.Empty, Guid.Empty, _faker.Random.Enum<Gender>(), 1, 1, 1, 1);
            var service = new PokedexService(_pokemonRepository.Object, _notifier.Object);

            // Act
            await service.UpdatePokemonAsync(pokemon);

            // Asserts
            _notifier.Verify(n => n.Notify(It.IsAny<FluentValidationResult>()), Times.Once);
            _pokemonRepository.Verify(pr => pr.HasPokemonAsync(pokemon.Id), Times.Never);
            _pokemonRepository.Verify(pr => pr.GetByNameAsync(pokemon.Name), Times.Never);
            _pokemonRepository.Verify(pr => pr.Update(pokemon), Times.Never);
        }

        [Fact(DisplayName = "Deve lançar uma notificação e impedir o update quando o pokémon não for encontrado no banco.")]
        public async Task UpdatePokemon_WhenPokemonNotFound()
        {
            // Arrange
            var pokemon = new Pokemon(Guid.NewGuid().ToString(), Guid.NewGuid(), _faker.Random.Enum<Gender>(), 1, 1, 1, 1);
            var service = new PokedexService(_pokemonRepository.Object, _notifier.Object);

            // Act
            await service.UpdatePokemonAsync(pokemon);

            // Asserts
            _pokemonRepository.Verify(pr => pr.HasPokemonAsync(pokemon.Id), Times.Once);
            _notifier.Verify(n => n.Notify("Não foi possivel encontrar o pokémon informado."), Times.Once);
            _pokemonRepository.Verify(pr => pr.GetByNameAsync(pokemon.Name), Times.Never);
            _pokemonRepository.Verify(pr => pr.Update(pokemon), Times.Never);
        }

        [Fact(DisplayName = "Deve atualizar o pokémon no banco quando existir um cadastro prévio do mesmo.")]
        public async Task UpdatePokemon_WhenSuccess()
        {
            // Arrange
            var pokemon = new Pokemon(Guid.NewGuid().ToString(), Guid.NewGuid(), _faker.Random.Enum<Gender>(), 1, 1, 1, 1);
            var service = new PokedexService(_pokemonRepository.Object, _notifier.Object);

            _pokemonRepository.Setup(pr => pr.HasPokemonAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            // Act
            await service.UpdatePokemonAsync(pokemon);

            // Asserts
            _pokemonRepository.Verify(pr => pr.HasPokemonAsync(pokemon.Id), Times.Once);
            _pokemonRepository.Verify(pr => pr.GetByNameAsync(pokemon.Name), Times.Once);
            _pokemonRepository.Verify(pr => pr.Update(pokemon), Times.Once);
        }

        [Fact(DisplayName = "Deve lançar uma notificação e impedir o update quando já existir um cadastro com o mesmo nome do pokémon informado.")]
        public async Task UpdatePokemon_WhenPokemonNameDuplicated()
        {
            // Arrange
            var pokemon = new Pokemon("Lucario", Guid.NewGuid(), _faker.Random.Enum<Gender>(), 1, 1, 1, 1);
            var service = new PokedexService(_pokemonRepository.Object, _notifier.Object);

            _pokemonRepository.Setup(pr => pr.HasPokemonAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _pokemonRepository.Setup(pr => pr.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Pokemon("Lucario", Guid.Empty, _faker.Random.Enum<Gender>(), 1, 1, 1, 1));

            // Act
            await service.UpdatePokemonAsync(pokemon);

            // Asserts
            _notifier.Verify(n => n.Notify("Não é possivel alterar o nome desse pokémon pois já existe um outro cadastro com o mesmo nome"), Times.Once);
            _pokemonRepository.Verify(pr => pr.Update(It.IsAny<Pokemon>()), Times.Never);
        }

        [Fact(DisplayName = "Deve lançar uma notificação e impedir a remoção quando o pokémon não for encontrado no banco")]
        public async Task DeletePokemon_WhenPokemonNotFindable()
        {
            // Arrange
            var pokemon = new Pokemon(Guid.NewGuid().ToString(), Guid.NewGuid(), _faker.Random.Enum<Gender>(), 1, 1, 1, 1);
            var service = new PokedexService(_pokemonRepository.Object, _notifier.Object);

            // Act
            await service.DeletePokemonAsync(pokemon.Id);

            // Asserts
            _notifier.Verify(n => n.Notify("Não foi possivel encontrar o pokémon informado."), Times.Once);
            _pokemonRepository.Verify(pr => pr.Delete(pokemon.Id), Times.Never);
        }

        [Fact(DisplayName = "Deve remover o pokémon do banco quando existir um cadastro prévio do mesmo.")]
        public async Task DeletePokemon_WhenExistingPokemon()
        {
            // Arrange
            var pokemon = new Pokemon(Guid.NewGuid().ToString(), Guid.NewGuid(), _faker.Random.Enum<Gender>(), 1, 1, 1, 1);
            var service = new PokedexService(_pokemonRepository.Object, _notifier.Object);

            _pokemonRepository.Setup(pr => pr.HasPokemonAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            // Act
            await service.DeletePokemonAsync(pokemon.Id);

            // Asserts
            _pokemonRepository.Verify(pr => pr.Delete(pokemon.Id), Times.Once);
        }
    }
}