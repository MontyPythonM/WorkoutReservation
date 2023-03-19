using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;

internal sealed class UpdateWorkoutTypeCommandValidatior : AbstractValidator<UpdateWorkoutTypeCommand>
{
    public UpdateWorkoutTypeCommandValidatior()
    {
        RuleFor(x => x.Intensity).IsInEnum();
    }      
}
