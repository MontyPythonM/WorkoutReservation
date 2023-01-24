using FluentValidation;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.UpdateWorkoutTypeTag;

public class UpdateWorkoutTypeTagCommandValidator : AbstractValidator<UpdateWorkoutTypeTagCommand>
{
    public UpdateWorkoutTypeTagCommandValidator()
    {
        RuleFor(x => x.Tag)
            .MaximumLength(50)
            .NotEmpty();
    }
}