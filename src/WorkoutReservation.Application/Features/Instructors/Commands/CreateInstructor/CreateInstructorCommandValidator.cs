using FluentValidation;

namespace WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor;

public class CreateInstructorCommandValidator : AbstractValidator<CreateInstructorCommand>
{
    public CreateInstructorCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(x => x.Biography)
            .MaximumLength(3000);

        RuleFor(x => x.Gender)
            .IsInEnum();
    }
}
