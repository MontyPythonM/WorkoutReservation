using WorkoutReservation.Domain.Exceptions;

namespace WorkoutReservation.Domain.Extensions;

public static class GuidExtension
{
    public static Guid ToGuid(this string inputValue)
    {
        if (Guid.TryParse(inputValue, out var parsedResult))
        {
            return parsedResult;
        }

        throw new DomainException("Cannot parse input string value to guid");
    }
}