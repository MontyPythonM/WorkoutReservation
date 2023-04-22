using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class InstructorCannotBeNullException : DomainException
{
    public InstructorCannotBeNullException() 
        : base("Instructor cannot be null")
    {
    }
}