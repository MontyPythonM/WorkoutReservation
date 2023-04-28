using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class WorkoutDateExceededException : DomainException
{
    public WorkoutDateExceededException() 
        : base("The date cannot be further than 2 weeks")
    {
    }
}