using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class DuplicateReservationException : DomainException
{
    public DuplicateReservationException() 
        : base("Application user can't have more than one reservation in 'reserved' status per real workout")
    {
    }
}