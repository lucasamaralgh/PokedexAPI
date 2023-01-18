using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.Models;
using Pokedex.Business.Entities;
using Pokedex.Business.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Pokedex.Api.Controllers
{
    [Route("pokedex")]
    public class PokedexController : ControllerBase
    {
        private readonly IPokedexService _pokedexService;
        private readonly IMapper _mapper;


        public PokedexController(IPokedexService pokedexService, IMapper mapper)
        {
            _pokedexService = pokedexService;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation("Cadastrar novo pokémon")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddPokemon([FromBody] PokemonModel model)
        {
            var pokemon = _mapper.Map<Pokemon>(model);

            var pokemonId = await _pokedexService.AddPokemon(pokemon);

            if (pokemonId == null) 
            { 
                return NotFound(); 
            }

            return Created($"{HttpContext.Request.Path}/{pokemonId}", null);
        }

        [HttpPut]
        [SwaggerOperation("Atualizar cadastro de pokémon")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePokemon([FromBody] PokemonModel model)
        {
            var pokemon = _mapper.Map<Pokemon>(model);
            await _pokedexService.UpdatePokemon(pokemon);

            return NoContent();
        }

        [HttpDelete("{pokemonId:guid}")]
        [SwaggerOperation("Remover pokémon")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePokemon(Guid pokemonId)
        {
            await _pokedexService.DeletePokemon(pokemonId);

            return NoContent();
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
