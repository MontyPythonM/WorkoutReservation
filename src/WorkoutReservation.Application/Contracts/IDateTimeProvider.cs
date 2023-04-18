namespace WorkoutReservation.Application.Contracts;

public interface IDateTimeProvider
{
    public DateTime Now { get; }
    public DateOnly CalculateDateInCurrentWeek(DayOfWeek dayOfWeek);
    public DateOnly CalculateDateInUpcomingWeek(DayOfWeek dayOfWeek);
    public DateTime CalculateDateTimeInCurrentWeek(DayOfWeek dayOfWeek, TimeOnly timeOnly);
    public bool CheckIsExpired(DateOnly date, TimeOnly time);
    public bool CheckIsExpired(DateTime dateTime);
    public DateOnly GetFirstDayOfCurrentWeek();
    public DateOnly GetLastDayOfCurrentWeek();
    public DateOnly GetFirstDayOfUpcomingWeek();
    public DateOnly GetLastDayOfUpcomingWeek();
    public DateOnly GetFirstDayOfCurrentWeekAndAddDays(int days);
}