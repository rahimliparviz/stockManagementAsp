using FluentValidation;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Stock.Services.DTO;

namespace Stock.Services.Validations.Supplier
{
    public class CreateSupplierDtoValidation:AbstractValidator<CreateSupplierDto>
    {
        public CreateSupplierDtoValidation()
        {
            //TODO: create custom rule for validation unique name
            RuleFor(c=>c.Name)
                .NotEmpty();
            RuleFor(c => c.Phone)
                .NotEmpty();
            RuleFor(c => c.Email)
                .EmailAddress()
                .NotEmpty();

        }
    }
}