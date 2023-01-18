using FluentValidation.Results;

namespace Pokedex.Business.Core
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateAt { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();

        }

        public virtual ValidationResult Validated()
        {
            throw new NotImplementedException("Override the validate method with valid conditions");
        }

    }
}
