using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace WorkoutReservation.Application.Features.Users.Commands.SelfUserDelete;

public class SelfDeleteUserCommandValidator : AbstractValidator<SelfDeleteUserCommand>
{
    public SelfDeleteUserCommandValidator(PasswordVerificationResult passwordCompareResult)
    {
        RuleFor(x => x.Password)
            .NotEmpty()                
            .Custom((value, context) => 
            {
                if (passwordCompareResult == PasswordVerificationResult.Failed)
                {
                    context.AddFailure("Invalid password.");
                }
            });
    }
}
