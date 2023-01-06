namespace WorkoutReservation.Domain.Extensions;

public static class DateTimeExtension
{
    public static DateOnly GetFirstDayOfWeek(this DateTime dateTime)
    {
        var currentDayDate = dateTime;
        var currentDayOfWeek = currentDayDate.DayOfWeek;

        var firstDayOfCurrentWeek = 
            currentDayDate.AddDays((- 7 + (1 - (int)currentDayOfWeek)) % 7);

        return DateOnly.FromDateTime(firstDayOfCurrentWeek);
    }
}
