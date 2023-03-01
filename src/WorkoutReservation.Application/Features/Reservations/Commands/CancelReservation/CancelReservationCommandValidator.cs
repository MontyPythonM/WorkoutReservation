using FluentValidation;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Commands.CancelReservation;

internal sealed class CancelReservationCommandValidator : AbstractValidator<CancelReservationCommand>
{
    public CancelReservationCommandValidator(Reservation reservation, Guid userGuid)
    {
        RuleFor(x => x.ReservationId)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(x => x).Custom((value, context) =>
        {
            if (reservation.User.Id != userGuid)
            {
                context.AddFailure("Access forbidden.");
            }
            
            if (reservation.RealWorkout.Date < DateOnly.FromDateTime(DateTime.Now.Date))
            {
                context.AddFailure($"Workout with Id: {reservation.RealWorkout.Id} has already taken place. You cannot cancel that reservation.");
            }

            if (reservation.ReservationStatus != ReservationStatus.Reserved)
            {
                context.AddFailure("ReservationStatus", "Reservation is no longer in reserved status");
            }
        });
    }
}
