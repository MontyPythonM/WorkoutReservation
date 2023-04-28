using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class ReservationsLessThanZeroException : DomainException
{
    public ReservationsLessThanZeroException() 
        : base("Reservations list cannot be less than zero")
    {
    }
}