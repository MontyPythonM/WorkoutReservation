using FluentValidation;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;

internal sealed class CreateWorkoutTypeCommandValidator : AbstractValidator<CreateWorkoutTypeCommand>
{
    public CreateWorkoutTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(x => x.Intensity)
            .IsInEnum()
            .NotEmpty();
        
        RuleFor(x => x.Description)
            .MaximumLength(600)
            .NotEmpty();
    }
}
