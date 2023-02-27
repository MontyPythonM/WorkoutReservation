using FluentValidation;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Features.Reservations.Commands.AddReservation;

internal sealed class AddReservationCommandValidator : AbstractValidator<AddReservationCommand>
{
    public AddReservationCommandValidator(RealWorkout realWorkout, bool isUserAlreadyReservedWorkout)
    {
        RuleFor(x => x.RealWorkoutId).NotEmpty();

        RuleFor(x => x).Custom((value, context) => 
        {
            if (realWorkout.CurrentParticipantNumber >= realWorkout.MaxParticipantNumber)
            {
                context.AddFailure($"The maximum number of participants on training with Id: {realWorkout.Id} was reached. " +
                    $"[{realWorkout.CurrentParticipantNumber}/{realWorkout.MaxParticipantNumber}]");
            }
        });

        RuleFor(x => x).Custom((value, context) =>
        {
            if (realWorkout.Date <= DateOnly.FromDateTime(DateTime.Now.Date) && realWorkout.EndTime < TimeOnly.FromDateTime(DateTime.Now))
            {
                context.AddFailure($"Training Id: {realWorkout.Id} has already taken place.");
            }
        });

        RuleFor(x => x).Custom((value, context) =>
        {
            if(isUserAlreadyReservedWorkout)
            {
                context.AddFailure($"User cannot reserve more than once for the same training.");
            }
        });
    }
}
