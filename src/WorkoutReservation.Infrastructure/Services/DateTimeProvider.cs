using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Infrastructure.Exceptions;

namespace WorkoutReservation.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now { get; } = DateTime.Now;

    public DateOnly CalculateDateInCurrentWeek(DayOfWeek dayOfWeek)
    {
        var firstDayOfCurrentWeek = GetFirstDayOfCurrentWeek();
        return dayOfWeek switch
        {
            DayOfWeek.Monday => firstDayOfCurrentWeek,
            DayOfWeek.Tuesday => firstDayOfCurrentWeek.AddDays(1),
            DayOfWeek.Wednesday => firstDayOfCurrentWeek.AddDays(2),
            DayOfWeek.Thursday => firstDayOfCurrentWeek.AddDays(3),
            DayOfWeek.Friday => firstDayOfCurrentWeek.AddDays(4),
            DayOfWeek.Saturday => firstDayOfCurrentWeek.AddDays(5),
            DayOfWeek.Sunday => firstDayOfCurrentWeek.AddDays(6),
            _ => throw new InfrastructureException("DayOfWeek value out of range")
        };
    }

    public DateOnly CalculateDateInUpcomingWeek(DayOfWeek dayOfWeek) =>
        CalculateDateInCurrentWeek(dayOfWeek).AddDays(7);
    
    public DateTime CalculateDateTimeInCurrentWeek(DayOfWeek dayOfWeek, TimeOnly timeOnly) => 
        CalculateDateInCurrentWeek(dayOfWeek).ToDateTime(timeOnly);

    public bool CheckIsExpired(DateOnly date, TimeOnly time) => Now > date.ToDateTime(time);
    
    public bool CheckIsExpired(DateTime dateTime) => Now > dateTime;

    public DateOnly GetFirstDayOfCurrentWeek() => 
        DateOnly.FromDateTime(CalculateFirstDayOfWeek(Now));

    public DateOnly GetLastDayOfCurrentWeek() =>
        DateOnly.FromDateTime(CalculateFirstDayOfWeek(Now)).AddDays(6);

    public DateOnly GetFirstDayOfUpcomingWeek() => 
        DateOnly.FromDateTime(CalculateFirstDayOfWeek(Now)).AddDays(7);
    
    public DateOnly GetLastDayOfUpcomingWeek() => 
        DateOnly.FromDateTime(CalculateFirstDayOfWeek(Now)).AddDays(13);
    
    public DateOnly GetFirstDayOfCurrentWeekAndAddDays(int days) => 
        DateOnly.FromDateTime(CalculateFirstDayOfWeek(Now)).AddDays(days);
    
    private static DateTime CalculateFirstDayOfWeek(DateTime dateTime) => 
        dateTime.AddDays((-7 + (1 - (int)dateTime.DayOfWeek)) % 7);
}