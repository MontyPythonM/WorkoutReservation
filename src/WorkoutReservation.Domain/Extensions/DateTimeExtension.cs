namespace WorkoutReservation.Domain.Extensions;

public static class DateTimeExtension
{
    public static DateOnly GetFirstDayOfWeek(this DateTime dateTime)
    {
        var firstDayOfWeek = CalculateFirstDayOfWeek(dateTime);
        return DateOnly.FromDateTime(firstDayOfWeek);
    }
    
    public static DateOnly GetFirstDayOfWeekAndAddDays(this DateTime dateTime, int days)
    {
        var firstDayOfWeek = CalculateFirstDayOfWeek(dateTime);
        return DateOnly.FromDateTime(firstDayOfWeek).AddDays(days);
    }

    private static DateTime CalculateFirstDayOfWeek(DateTime dateTime) => 
        dateTime.AddDays((-7 + (1 - (int)dateTime.DayOfWeek)) % 7);
}
