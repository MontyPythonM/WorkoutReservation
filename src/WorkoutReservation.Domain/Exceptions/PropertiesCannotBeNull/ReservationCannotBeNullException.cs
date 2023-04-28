using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class ReservationCannotBeNullException : DomainException
{
    public ReservationCannotBeNullException() : base("Reservation cannot be null")
    {
    }
}