using FluentValidation;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType
{
    public class CreateWorkoutTypeCommandValidator : AbstractValidator<CreateWorkoutTypeCommand>
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
                .MaximumLength(5000)
                .NotEmpty();
        }
    }
}
