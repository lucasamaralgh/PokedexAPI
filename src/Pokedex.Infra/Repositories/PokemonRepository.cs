using Pokedex.Business.Entities;
using Pokedex.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Infra.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
       public Task AddAsync(Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

       public void Update(Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

       public void Delete(Guid pokemonId)
        {
            throw new NotImplementedException();
        }

       public Task<bool> HasPokemonAsync(Guid pokemonID)
        {
            throw new NotImplementedException();
        }

       public Task<Pokemon> GetByIdAsync(Guid pokemonID)
        {
            throw new NotImplementedException();
        }

       public Task<Pokemon> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

       public Task<IEnumerable<Pokemon>> FindAsync()
        {
            throw new NotImplementedException();
        }
    }
}
