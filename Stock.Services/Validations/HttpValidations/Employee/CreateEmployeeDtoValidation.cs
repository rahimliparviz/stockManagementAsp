using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Stock.Services.DTO;
using Stock.Services.Validations.HttpValidations.Extensions;

namespace Stock.Services.Validations.HttpValidations.Employee
{
    public class CreateEmployeeDtoValidation:AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidation()
        {
            // When(e => e.IsUpdating  && e.Password != "", () =>
            // {
            //     RuleFor(c => c.Password)
            //         .NotEmpty()
            //         .Password();
            // });
            //
            // When(e => e.IsUpdating == false, () =>
            // {
            //     RuleFor(c => c.Password)
            //         .NotEmpty()
            //         .Password();
            // });
            
            RuleFor(c=>c.UserName)
                .NotEmpty();
            RuleFor(c=>c.Password)
                .NotEmpty()
                .Password().When(e=>(e.IsUpdating  && !string.IsNullOrWhiteSpace(e.Password)) || !e.IsUpdating)

                ;
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