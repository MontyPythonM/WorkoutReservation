using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Account.Commands.Register;

internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(ApplicationUser user)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Custom((value, context) => 
            {
                if (user is not null)
                {
                    context.AddFailure("Email address is already taken.");
                }
            });

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(50);

        RuleFor(x => x.ConfirmPassword)
            .Equal(e => e.Password)
            .WithMessage("'ConfirmPassword' must be equal 'Password'.");

        RuleFor(x => x.FirstName).MaximumLength(50); 

        RuleFor(x => x.LastName).MaximumLength(50);

        RuleFor(x => x.Gender).IsInEnum();

        RuleFor(x => x.DateOfBirth).Custom((value, context) =>
        { 
            if (value > DateOnly.FromDateTime(DateTime.Now))
            {
                context.AddFailure("You cannot be born in the future");
            }
        });
    }
}
