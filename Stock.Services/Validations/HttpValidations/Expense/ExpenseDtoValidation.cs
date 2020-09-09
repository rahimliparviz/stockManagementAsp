using FluentValidation;
using Stock.Services.DTO;

namespace Stock.Services.Validations.Expense
{
    public class ExpenseDtoValidation:AbstractValidator<ExpenseDto>
    {
        public ExpenseDtoValidation()
        {
            RuleFor(x=>x.Amount)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Details)
                .NotEmpty();
        }
    }
}