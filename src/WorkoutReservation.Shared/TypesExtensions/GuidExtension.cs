using WorkoutReservation.Shared.Exceptions;

namespace WorkoutReservation.Shared.TypesExtensions;

public static class GuidExtension
{
    public static Guid ToGuid(this string inputValue)
    {
        return Guid.TryParse(inputValue, out var parsedResult) ? parsedResult : throw new ConversionException();
    }
    
    public static Guid? ToNullableGuid(this string inputValue)
    {
        return Guid.TryParse(inputValue, out var parsedValue) ? parsedValue : null;
    }
}