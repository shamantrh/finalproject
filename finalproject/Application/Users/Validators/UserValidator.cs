using finalproject.Application.Users.Commands;
using FluentValidation;

namespace finalproject.Application.Users.Validators
{
    public class UserValidator:AbstractValidator<AddUserCommand>
    {
        public UserValidator()
        {
            RuleFor(u => u.name)
            .NotEmpty().WithMessage("Name is Required")
            .MinimumLength(2).WithMessage("The name should have atleast 2 letters");

            RuleFor(u => u.email)
            .NotEmpty().WithMessage("The Email field should not be empty.")
            .Matches(@"^[a-z0-9\._%+-]+@(leadsquared\.com|gmail\.com)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            .WithMessage("Enter a suitable email format here");

          
        }
    }
}
