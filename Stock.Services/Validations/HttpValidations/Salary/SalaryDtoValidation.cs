using FluentValidation;
using Stock.Services.DTO;
using Stock.Services.Validations.HttpValidations.Extensions;

namespace Stock.Services.Validations.HttpValidations.Salary
{
    public class SalaryDtoValidation:AbstractValidator<SalaryDto>
    {

        public SalaryDtoValidation()
        {
          
            RuleFor(s => s.Amount)
                .NotNull()
                .GreaterThan(0);
            RuleFor(s => s.Month)
                .NotEmpty()
                .InclusiveBetween(1,12);
            RuleFor(s => s.EmployeeId)
                .NotEmpty();
            // RuleFor(s => s.SalaryDate)
            //     .NotEmpty().When(c=>!c.IsUpdating);
            RuleFor(s => s.Year)
                .Year()
                .When(c=>!c.IsUpdating)
                ;
        }
    }
}