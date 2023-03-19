using FluentValidation;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;

internal sealed class CreateWorkoutTypeCommandValidator : AbstractValidator<CreateWorkoutTypeCommand>
{
    public CreateWorkoutTypeCommandValidator()
    {
        RuleFor(x => x.Instructors)
            .NotEmpty();
        
        RuleFor(x => x.WorkoutTypeTags)
            .NotEmpty();

        RuleFor(x => x.Intensity)
            .IsInEnum();
    }
}
