using FluentValidation;
using Stock.Services.DTO;

namespace Stock.Services.HttpValidations.Product
{
    public class ProductDtoValidation:AbstractValidator<CreateProductDto>
    {
        public ProductDtoValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty();
            RuleFor(p => p.Code)
                .NotEmpty();
            RuleFor(p => p.BuyingPrice)
                .NotEmpty()
                // .EmailAddress()
                ;
            RuleFor(p => p.SelingPrice)
                .NotEmpty().NotNull()
                // .EmailAddress()
                ;
            RuleFor(p => p.CategoryId)
                .NotEmpty();
            RuleFor(p => p.SupplierId)
                .NotEmpty()
                .NotNull();
       
            RuleFor(p => p.BuyingDate)
                .NotEmpty();
            RuleFor(p => p.Quantity)
                .NotEmpty()
                .GreaterThan(0)
                ;
        }
    }
}