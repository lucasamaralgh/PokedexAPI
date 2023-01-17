using FluentValidation.Results;
using Pokedex.Business.Core;
using Pokedex.Business.Enums;

namespace Pokedex.Business.Entities
{
    public  class Pokemon : Entity
    {
        public String Name { get; private set; }

        public Guid CategoryId { get; private set; }

        public Gender Gender { get; private set; }

        public int Hp { get; private set; }

        public int Attack { get; private set; }

        public int Defense { get; private set; }

        public int Speed { get; private set; }

        public Pokemon(string name, Guid categoryId, Gender gender, int hp, int attack, int defense, int speed)
        {
            Name = name;
            CategoryId = categoryId;
            Gender = gender;
            Hp = hp;
            Attack = attack;
            Defense = defense;
            Speed = speed;
        }

        public override ValidationResult Validated()
        {
            return new ValidationResult();
        }
    }
}