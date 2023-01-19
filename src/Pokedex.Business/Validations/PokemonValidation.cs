﻿using FluentValidation;
using Pokedex.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Validations
{
    public class PokemonValidation : AbstractValidator<Pokemon>
    { 
        public PokemonValidation()
        {
            RuleFor(p => p.Name)
                .Length(3, 50)
                .WithMessage("O nome do pokemon deve ter entre {MinLenght} e {MaxLenght} caracteres.");

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .WithMessage("É necessário informar uma categoria válida");

            RuleFor(p => p.Hp)
                .GreaterThan(0)
                .WithMessage("O HP deve ser maior que zero");

            RuleFor(p => p.Attack)
                .GreaterThan(0)
                .WithMessage("O Ataque deve ser maior que zero");

            RuleFor(p => p.Defense)
                .GreaterThan(0)
                .WithMessage("A Defesa deve ser maior que zero");

            RuleFor(p => p.Speed)
                .GreaterThan(0)
                .WithMessage("A velocidade deve ser maior que zero");
        }
    }
}
