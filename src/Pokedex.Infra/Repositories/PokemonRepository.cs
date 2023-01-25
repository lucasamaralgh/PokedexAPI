using Microsoft.EntityFrameworkCore;
using Pokedex.Business.Entities;
using Pokedex.Business.Queries;
using Pokedex.Business.Repositories;
using Pokedex.Infra.Core;

namespace Pokedex.Infra.Repositories
{
    public class PokemonRepository : RepositoryBase, IPokemonRepository
    {
        public PokemonRepository(EFDbContext efContext) : base(efContext) 
        { }

        public async Task AddAsync(Pokemon pokemon)
        {
            await _efContext.Pokemons.AddAsync(pokemon);
            _efContext.SaveChanges();
        }

        public void Update(Pokemon pokemon)
        {
            _efContext.Pokemons.Update(pokemon);
        }

        public void Delete(Guid pokemonId)
        {
            _efContext.Pokemons
                .Where(p => p.Id == pokemonId)
                .ExecuteDelete();
        }

        public Task<bool> HasPokemonAsync(Guid pokemonID)
        {
            return _efContext.Pokemons.AnyAsync(p => p.Id == pokemonID);
        }

        public Task<Pokemon?> GetByIdAsync(Guid pokemonID)
        {
            return _efContext.Pokemons
                .FirstOrDefaultAsync(p => p.Id == pokemonID);
        }

        public Task<Pokemon?> GetByNameAsync(string name)
        {
            return _efContext.Pokemons.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Pokemon>> FindAsync(FindPokemonQuery query)
        {
            var findQuery = _efContext.Pokemons.AsQueryable();

            if (query.HasName)
                findQuery = findQuery.Where(p => p.Name == query.Name);

            if (query.HasCategory)
                findQuery = findQuery.Where(p => p.CategoryId == query.CategoryId);

            return await findQuery.ToListAsync();
        }
    }
}
