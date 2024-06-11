using Bogus;
using FluentAssertions;
using Pokedex.Business.Entities;
using Pokedex.Business.Enums;
using Xunit;

namespace Pokedex.Tests.Entities
{
    public class PokemonTests
    {
        private readonly Faker _faker;

        public PokemonTests() { _faker = new Faker(); }

        [Fact(DisplayName = "Deve construir pokemon quando o objeto for instanciado")]
        public void ValidarConstrutorPokemon()
        {
            //Arrange
            var id = Guid.NewGuid();
            var gender = _faker.Random.Enum<Gender>();

            //Act
            var pokemon = new Pokemon("Lucario", id, gender, 1, 1, 1, 1);

            //Asserts
            pokemon.Name.Should().Be("Lucario");
            pokemon.CategoryId.Should().Be(id);
            pokemon.Gender.Should().Be(gender); 

        }

        [Fact(DisplayName = "Deve validar com sucesso pokemon quando o mesmo for valido")]
        public void ValidarValidacaoDePokemonValido()
        {
            //Arrange
            var guild = Guid.NewGuid();
            var gender = _faker.Random.Enum<Gender>();
            var pokemon = new Pokemon("Lucario", guild, gender, 1, 1, 1, 1);

            //Act
            var validate = pokemon.Validate();

            //Asserts
            validate.IsValid.Should().BeTrue();

        }

    }
}
