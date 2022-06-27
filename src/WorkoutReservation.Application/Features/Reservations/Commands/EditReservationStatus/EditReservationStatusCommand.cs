using MediatR;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.Features.Reservations.Commands.EditReservationStatus
{
    public class EditReservationStatusCommand : IRequest
    {
        public int ReservationId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }
}
