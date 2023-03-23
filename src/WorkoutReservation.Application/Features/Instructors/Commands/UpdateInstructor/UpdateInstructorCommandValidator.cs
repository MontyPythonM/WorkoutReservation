using FluentValidation;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Instructors.Commands.UpdateInstructor;

internal sealed class UpdateInstructorCommandValidator : AbstractValidator<UpdateInstructorCommand>
{
    public UpdateInstructorCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.Biography)
            .MaximumLength(3000);

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(x => x.Gender)
            .IsInEnum();
    }
}
