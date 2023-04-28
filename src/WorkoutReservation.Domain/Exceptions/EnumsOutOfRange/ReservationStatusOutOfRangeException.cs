using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class ReservationStatusOutOfRangeException : DomainException
{
    public ReservationStatusOutOfRangeException() 
        : base("ReservationStatus value cannot be out of enum range")
    {
    }
}