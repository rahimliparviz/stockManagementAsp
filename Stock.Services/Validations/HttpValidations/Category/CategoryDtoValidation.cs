using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Stock.Domain;
using Stock.Services.DTO;
using Stock.Services.HttpValidations.Extensions;

namespace Stock.Data.Validations.Category
{
    public class CategoryDtoValidation: AbstractValidator<CategoryDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        
        public CategoryDtoValidation(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            var categories = _mapper.Map<List<Domain.Category>, List<CategoryDto>>(_context.Categories.ToList());
            
            RuleFor(x => x.Name)
              .NotEmpty()
              // .SetValidator(new UniqueValidator<CategoryDto>(categories))
              ;
        }
    }
}