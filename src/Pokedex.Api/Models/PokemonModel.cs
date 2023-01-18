using Pokedex.Business.Enums;

namespace Pokedex.Api.Models
{
    public class PokemonModel
    {
        public String Name { get;  set; } = default!;

        public Guid CategoryId { get;  set; }

        public Gender Gender { get;  set; }

        public int Hp { get;  set; }

        public int Attack { get;  set; }

        public int Defense { get;  set; }

        public int Speed { get;  set; }
    }
}
