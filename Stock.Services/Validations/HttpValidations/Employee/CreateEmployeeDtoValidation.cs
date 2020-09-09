using FluentValidation;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Stock.Services.DTO;

namespace Stock.Services.Validations.Employee
{
    public class CreateEmployeeDtoValidation:AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidation()
        {
            //TODO: create custom rule for validation unique name
            RuleFor(c=>c.Name)
                .NotEmpty();
            RuleFor(c => c.Phone)
                .NotEmpty();
            RuleFor(c => c.Email)
                .EmailAddress()
                .NotEmpty();
            RuleFor(c => c.Address)
                .NotEmpty();
            RuleFor(c => c.JoiningDate)
                .NotEmpty();
            RuleFor(c => c.Salary)
                .NotEmpty()
                .GreaterThan(0);

        }
    }
}