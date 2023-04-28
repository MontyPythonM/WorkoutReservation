using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class WorkoutTypeTagCannotBeNullOfWhitespace : DomainException
{
    public WorkoutTypeTagCannotBeNullOfWhitespace() 
        : base("Tag cannot be null")
    {
    }
}