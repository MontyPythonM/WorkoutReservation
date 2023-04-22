using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class WorkoutTypeNameCannotBeNullOrWhiteSpace : DomainException
{
    public WorkoutTypeNameCannotBeNullOrWhiteSpace() 
        : base("WorkoutType name type cannot be null")
    {
    }
}