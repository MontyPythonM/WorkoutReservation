using FluentValidation;

namespace WorkoutReservation.Application.Features.Users.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {

        }
    }
}
