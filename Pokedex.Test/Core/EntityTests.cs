using FluentAssertions;
using Pokedex.Tests.Common;
using Xunit;

namespace Pokedex.Tests.Core
{
    public class EntityTests
    {
        [Fact(DisplayName = "Deve inicializar o Id quando o objeto for instanciado.")]
        public void Constructor()
        {
            var pokemon = new EntityWithoutValidation();
            pokemon.Id.Should().NotBeEmpty();
        }
    }
}