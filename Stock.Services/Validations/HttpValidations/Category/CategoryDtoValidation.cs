using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Stock.Domain;
using Stock.Services.DTO;

namespace Stock.Data.Validations.Category
{
    public class CategoryDtoValidation: AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidation()
        {
            RuleFor(x => x.Name)
              .NotEmpty()
              // .SetValidator(new UniqueValidator<CategoryDto>(categories))
              ;
        }
    }
}