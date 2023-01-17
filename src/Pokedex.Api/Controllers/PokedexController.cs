using Microsoft.AspNetCore.Mvc;
using Pokedex.Business.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Pokedex.Api.Controllers
{
    [Route("pokedex")]
    public class PokedexController : ControllerBase
    {
        [HttpPost]
        [SwaggerOperation("Cadastrar novo pokémon")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddPokemon()
        {

            return Ok();
        }

        [HttpPut]
        [SwaggerOperation("Atualizar cadastro de pokémon")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePokemon()
        {

            return Ok();
        }

        [HttpDelete]
        [SwaggerOperation("Remover pokémon")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePokemon()
        {

            return Ok();
        }

        [HttpGet("{pokedexId:guid}")]
        [SwaggerOperation("Obter pokémon por Id")]
        [ProducesResponseType(typeof(Pokemon), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPokemonById()
        {

            return Ok();
        }

        [HttpGet("find")]
        [SwaggerOperation("Listar pokémons")]
        [ProducesResponseType(typeof(IEnumerable<Pokemon>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindPokemon()
        {

            return Ok();
        }
    }
}
