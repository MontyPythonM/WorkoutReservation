namespace WorkoutReservation.Shared.TypesExtensions;

public static class DateTimeExtension
{
    public static DateOnly GetFirstDayOfCurrentWeek(this DateTime dateTime) =>
        DateOnly.FromDateTime(CalculateFirstDayOfWeek(dateTime));
    
    public static DateOnly GetLastDayOfCurrentWeek(this DateTime dateTime) =>
        DateOnly.FromDateTime(CalculateFirstDayOfWeek(dateTime)).AddDays(6);

    public static DateOnly GetFirstDayOfUpcomingWeek(this DateTime dateTime) =>
        DateOnly.FromDateTime(CalculateFirstDayOfWeek(dateTime)).AddDays(7);

    public static DateOnly GetLastDayOfUpcomingWeek(this DateTime dateTime) => 
        DateOnly.FromDateTime(CalculateFirstDayOfWeek(dateTime)).AddDays(13);
    
    public static bool IsExpired(this DateTime now, DateOnly date, TimeOnly time) => now > date.ToDateTime(time);
    public static bool IsExpired(this DateTime now, DateTime dateTime) => now > dateTime;
    
    private static DateTime CalculateFirstDayOfWeek(DateTime dateTime) => 
        dateTime.AddDays((-7 + (1 - (int)dateTime.DayOfWeek)) % 7);
}
