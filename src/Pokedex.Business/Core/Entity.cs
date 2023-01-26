﻿using FluentValidation.Results;

namespace Pokedex.Business.Core
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();

        }

        public void SetCreationDate(DateTime date)
        {
            CreatedAt = date;
        }

        public void SetUpdateDate(DateTime date)
        {
            UpdatedAt = date;
        }

        public virtual ValidationResult Validate()
        {
            throw new NotImplementedException("Override the validate method with valid conditions");
        }

    }
}
