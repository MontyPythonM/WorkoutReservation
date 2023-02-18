using FluentValidation;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.CreateWorkoutTypeTag;

public class CreateWorkoutTypeTagCommandValidator : AbstractValidator<CreateWorkoutTypeTagCommand>
{
    public CreateWorkoutTypeTagCommandValidator()
    {
        RuleFor(x => x.Tag)
            .MaximumLength(30)
            .NotEmpty();
    }
}