using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class ApplicationUserCannotBeNullException : DomainException
{
    public ApplicationUserCannotBeNullException() : base("Application user cannot be null")
    {
    }
}