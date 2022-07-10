using FluentValidation;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Instructors.Commands.UpdateInstructor;

public class UpdateInstructorCommandValidator : AbstractValidator<UpdateInstructorCommand>
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
            .NotNull();

        RuleFor(x => x.Gender)
            .Must(Gender => Gender >= (Gender)1 && Gender <= (Gender)2)
            .WithMessage("Gender must be number 1 or 2. [female / male]");
    }
}
