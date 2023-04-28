using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class ReservationAlreadyCancelledException : DomainException
{
    public ReservationAlreadyCancelledException() : base("Reservation is already cancelled")
    {
    }
}