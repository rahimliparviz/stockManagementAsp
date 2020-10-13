using FluentValidation;
using FluentValidation.Validators;

namespace Stock.Services.Validations.HttpValidations.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty()
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
                .Matches("[a-z]").WithMessage("Password must have at least 1 lowercase character")
                .Matches("[0-9]").WithMessage("Password must contain a number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");
        
            return options;
        }
        
        public static IRuleBuilderOptions<T, string> Year<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty()
                .Length(4)
                // .MinimumLength(6)
                // .Matches(@"[^\d{4}$]").
                .WithMessage("Year format is not correct");

            return options;
        }
        
        // public static IRuleBuilderOptions<T, string> Year<T>(this IRuleBuilder<T, string> ruleBuilder)
        // {
        //     return ruleBuilder
        //         .SetValidator(new RegularExpressionValidator(@"^[1-9]\d{3,}$"))
        //         .WithMessage("Year format is not correct")
        //         ;
        // }
    }
}