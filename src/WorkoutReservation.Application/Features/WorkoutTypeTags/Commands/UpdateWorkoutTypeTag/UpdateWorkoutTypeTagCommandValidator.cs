using FluentValidation;

namespace WorkoutReservation.Application.Features.WorkoutTypeTags.Commands.UpdateWorkoutTypeTag;

internal sealed class UpdateWorkoutTypeTagCommandValidator : AbstractValidator<UpdateWorkoutTypeTagCommand>
{
    public UpdateWorkoutTypeTagCommandValidator()
    {
        RuleFor(x => x.Tag)
            .MaximumLength(50)
            .NotEmpty();
    }
}