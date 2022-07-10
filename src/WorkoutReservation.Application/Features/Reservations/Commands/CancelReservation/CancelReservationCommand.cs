using MediatR;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Commands.CancelReservation;

public class CancelReservationCommand : IRequest
{
    public int ReservationId { get; set; }
}
