using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Queries
{
    public class FindPokemonQuery
    {
        public string Name { get; set; } = default!;

        public Guid? CategoryId { get; set; }

        public bool HasName => !string.IsNullOrWhiteSpace(Name);

        public bool HasCategory => CategoryId != null;
    }
}
