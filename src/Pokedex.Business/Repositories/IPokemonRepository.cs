﻿using Pokedex.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Repositories
{
    public interface IPokemonRepository
    {
        Task AddAsync(Pokemon pokemon);

        void UpdateAsync(Pokemon pokemon);

        void DeleteAsync(Pokemon pokemon);

        Task<bool> HasPokemonAsync(Guid pokemonID);

        Task<Pokemon> GetByIdAsync(Guid pokemonID);

        Task<Pokemon> GetByNameAsync(string name);

        Task<IEnumerable<Pokemon>> FindAsyn();
    }
}