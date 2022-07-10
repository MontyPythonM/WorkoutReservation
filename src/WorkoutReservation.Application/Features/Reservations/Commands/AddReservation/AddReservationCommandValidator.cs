using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;

public class AddReservationCommandValidator : AbstractValidator<AddReservationCommand>
{
    public AddReservationCommandValidator(RealWorkout realWorkout, 
                                          Guid currentUserGuid, 
                                          bool isUserAlreadyReservedWorkout)
    {
        RuleFor(x => x.RealWorkoutId).NotEmpty();

        RuleFor(x => x).Custom((value, context) => 
        {
            if (realWorkout.CurrentParticipianNumber >= realWorkout.MaxParticipianNumber)
            {
                context.AddFailure($"The maximum number of participants on training with Id: {realWorkout.Id} was reached. " +
                    $"[{realWorkout.CurrentParticipianNumber}/{realWorkout.MaxParticipianNumber}]");
            }
        });

        RuleFor(x => x).Custom((value, context) =>
        {
            if (realWorkout.Date < DateOnly.FromDateTime(DateTime.Now.Date))
            {
                context.AddFailure($"Training Id: {realWorkout.Id} has already taken place.");
            }
        });

        RuleFor(x => x).Custom((value, context) =>
        {
            if(isUserAlreadyReservedWorkout)
            {
                context.AddFailure($"User with Id: {currentUserGuid} cannot reserve more than once for the same training.");
            }
        });
    }
}
