﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.Models;
using Pokedex.Business.Core.Notifications;
using Pokedex.Business.Core.Pagination;
using Pokedex.Business.Entities;
using Pokedex.Business.Queries;
using Pokedex.Business.Repositories;
using Pokedex.Business.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Pokedex.Api.Controllers
{
    [Authorize]
    [Route("pokedex")]
    public class PokedexController : ControllerBase
    {
        private readonly IPokedexService _pokedexService;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        private readonly INotifier _notifier;


        public PokedexController(IPokedexService pokedexService, IPokemonRepository pokemonRepository, IMapper mapper, INotifier notifier)
        {
            _pokedexService = pokedexService;
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
            _notifier = notifier;
        }

        [HttpPost]
        [SwaggerOperation("Cadastrar novo pokémon")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddPokemon([FromBody] PokemonModel model)
        {
            var pokemon = _mapper.Map<Pokemon>(model);

            var pokemonId = await _pokedexService.AddPokemonAsync(pokemon);

            if (_notifier.HasNotification)
                UnprocessableEntity();

            return Created($"{HttpContext.Request.Path}/{pokemonId}", null);
        }

        [HttpPut]
        [SwaggerOperation("Atualizar cadastro de pokémon")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePokemon([FromBody] PokemonModel model)
        {
            var pokemon = _mapper.Map<Pokemon>(model);
            await _pokedexService.UpdatePokemonAsync(pokemon);

            return NoContent();
        }

        [HttpDelete("{pokemonId:guid}")]
        [SwaggerOperation("Remover pokémon")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePokemon(Guid pokemonId)
        {
            await _pokedexService.DeletePokemonAsync(pokemonId);

            return NoContent();
        }

        [HttpGet("{pokedexId:guid}")]
        [SwaggerOperation("Obter pokémon por Id")]
        [ProducesResponseType(typeof(Pokemon), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPokemonById(Guid pokemonId)
        {
            var pokemon =  await _pokedexService.GetByIdAsync(pokemonId);

            if (pokemon == null)
                return NotFound();

            var result = _mapper.Map<PokemonModel>(pokemon);
            
            return Ok(result);
        }

        [HttpGet("find")]
        [SwaggerOperation("Listar pokémons")]
        [ProducesResponseType(typeof(PagedList<Pokemon>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindPokemon(FindPokemonQuery query)
        {
            var pokemons = await _pokemonRepository.FindAsync(query);

            var result = _mapper.Map<PagedList<PokemonModel>>(pokemons);
            return Ok(result);
        }
    }
}
