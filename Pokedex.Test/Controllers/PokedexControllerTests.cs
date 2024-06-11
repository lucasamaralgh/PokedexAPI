using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pokedex.Api.Configurations;
using Pokedex.Api.Controllers;
using Pokedex.Api.Models;
using Pokedex.Business.Core.Notifications;
using Pokedex.Business.Entities;
using Pokedex.Business.Enums;
using Pokedex.Business.Repositories;
using Pokedex.Business.Services;
using Xunit;

namespace Pokedex.Tests.Controllers
{
    public class PokedexControllerTests
    {
        private readonly Mock<IPokemonRepository> _pokemonRepository;
        private readonly Mock<IPokedexService> _pokedexService;
        private readonly IMapper _mapper;
        private readonly Mock<INotifier> _notifier;
        private readonly Faker _faker;

        public PokedexControllerTests()
        {
            _pokemonRepository = new Mock<IPokemonRepository>();
            _faker = new Faker();
            _pokedexService = new Mock<IPokedexService>();
            _notifier = new Mock<INotifier>();
            _mapper = new MapperConfiguration(options =>
            {
                options.AddProfile<AutoMapperConfig>();
            }).CreateMapper();
        }

        [Fact(DisplayName = "Deve cadastar pokemon com sucesso quando pokemon for valido")]
        public async Task AddPokemon_Valid()
        {
            // Arrange
            var pokemonModel = new PokemonModel()
            {
                Name = Guid.NewGuid().ToString(),
                CategoryId = Guid.NewGuid(),
                Gender = _faker.Random.Enum<Gender>() 
            };

            var controller = CreateDefaultController();

            // Act
            var result = await controller.AddPokemon(pokemonModel) as ObjectResult;

            // Asserts
            _pokedexService.Verify(pr => pr.AddPokemonAsync(It.Is<Pokemon>(p =>
                p.Name == pokemonModel.Name
                && p.CategoryId == pokemonModel.CategoryId
                && p.Gender == pokemonModel.Gender)), Times.Once);

            result!.StatusCode.Should().Be(201);

            //verificar path retornado
        }

        [Fact(DisplayName = "Deve atualizar pokemon com sucesso quando pokemon for valido")]
        public async Task AtualizarPokemon_Valido()
        {
            // Arrange
            var pokemonModel = new PokemonModel()
            {
                Name = Guid.NewGuid().ToString(),
                CategoryId = Guid.NewGuid(),
                Gender = _faker.Random.Enum<Gender>()
            };

            var controller = CreateDefaultController();

            // Act
            var result = await controller.UpdatePokemon(pokemonModel) as NoContentResult;

            // Asserts
            _pokedexService.Verify(pr => pr.UpdatePokemonAsync(It.Is<Pokemon>(p =>
                p.Name == pokemonModel.Name
                && p.CategoryId == pokemonModel.CategoryId
                && p.Gender == pokemonModel.Gender)), Times.Once);

            result!.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Deve remover pokemon com sucesso quando pokemon for valido")]
        public async Task RemoverPokemon_Valido()
        {
            // Arrange
            var pokemon = new Pokemon(Guid.NewGuid().ToString(), Guid.NewGuid(), _faker.Random.Enum<Gender>(), 1, 1, 1, 1);

            _pokemonRepository.Setup(pr => pr.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            _pokemonRepository.Setup(pr => pr.HasPokemonAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            var controller = CreateDefaultController();

            // Act
            var result = await controller.DeletePokemon(pokemon.Id) as NoContentResult;

            // Asserts
            _pokedexService.Verify(pr => pr.DeletePokemonAsync(pokemon.Id), Times.Once);
            result!.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Deve obter pokemonID e retornar 200 com sucesso quando pokemon for valido")]
        public async Task ObterPokemonId_Valido()
        {
            // Arrange
            var pokemon = new Pokemon(Guid.NewGuid().ToString(), Guid.NewGuid(), _faker.Random.Enum<Gender>(), 1, 1, 1, 1);

            _pokedexService.Setup(ps => ps.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(pokemon);

            var controller = CreateDefaultController();

            // Act
            var result = await controller.GetPokemonById(pokemon.Id) as OkObjectResult;

            // Asserts
            _pokedexService.Verify(pr => pr.GetByIdAsync(pokemon.Id), Times.Once);
            result!.Value.Should().BeEquivalentTo(_mapper.Map<PokemonModel>(pokemon));
            result!.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact(DisplayName = "Deve obter pokemonID com sucesso e retornar 200 quando pokemon for valido")]
        public async Task ObterPokemonId_Invalido()
        {
            // Arrange
            var pokemonId = Guid.NewGuid();
            var controller = CreateDefaultController();

            // Act
            var result = await controller.GetPokemonById(pokemonId) as NotFoundResult;

            // Asserts
            _pokedexService.Verify(pr => pr.GetByIdAsync(pokemonId), Times.Once);
            result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        private PokedexController CreateDefaultController()
        {
            return new PokedexController(_pokedexService.Object, _pokemonRepository.Object, _mapper, _notifier.Object)
            {
                ControllerContext = new()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
        }

    }
}
