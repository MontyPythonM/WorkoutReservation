using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Shared.Exceptions;

public class ConversionException : Exception
{
    public ConversionException() : base("Input value cannot be converted")
    {
    }
}