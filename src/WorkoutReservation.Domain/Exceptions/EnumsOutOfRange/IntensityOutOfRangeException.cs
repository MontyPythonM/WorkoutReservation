using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Domain.Exceptions;

public class IntensityOutOfRangeException : DomainException
{
    public IntensityOutOfRangeException() : base("Intensity value cannot be out of enum range")
    {
    }
}