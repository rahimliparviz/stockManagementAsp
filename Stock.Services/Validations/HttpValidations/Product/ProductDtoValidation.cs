using FluentValidation;
using Stock.Services.DTO;

namespace Stock.Services.HttpValidations.Product
{
    public class ProductDtoValidation:AbstractValidator<ProductDto>
    {
        public ProductDtoValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty();
            RuleFor(p => p.Code)
                .NotEmpty();
            RuleFor(p => p.CategoryId)
                .NotEmpty();
            RuleFor(p => p.SupplierId)
                .NotEmpty();
            RuleFor(p => p.BuyingPrice)
                .NotEmpty();
            RuleFor(p => p.SelingPrice)
                .NotEmpty()
                ;
            RuleFor(p => p.BuyingDate)
                .NotEmpty();
            RuleFor(p => p.Quantity)
                .NotEmpty()
                .GreaterThan(0)
                ;
        }
    }
}