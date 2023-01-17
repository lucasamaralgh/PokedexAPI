using Microsoft.AspNetCore.Mvc;

namespace Pokedex.Api.Controllers
{
    [Route("pokedex")]
    public class PokedexController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddPokemon()
        {

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePokemon()
        {

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePokemon()
        {

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPokemonById()
        {

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> FindPokemon()
        {

            return Ok();
        }
    }
}
