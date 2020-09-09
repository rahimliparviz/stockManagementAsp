using FluentValidation;
using Stock.Data;
using Stock.Services.DTO;

namespace Stock.Services.HttpValidations.Salary
{
    public class SalaryDtoValidation:AbstractValidator<SalaryDto>
    {

        public SalaryDtoValidation()
        {
          
            RuleFor(s => s.Amount)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(s => s.Month)
                .NotEmpty()
                .InclusiveBetween(1,12);
            RuleFor(s => s.EmployeeId)
                .NotEmpty();
            RuleFor(s => s.SalaryDate)
                .NotEmpty();
            RuleFor(s => s.Year)
                .NotEmpty()
                .GreaterThan(0)
                ;
        }
    }
}