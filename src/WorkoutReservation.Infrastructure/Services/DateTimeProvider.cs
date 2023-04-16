using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Domain.Extensions;

namespace WorkoutReservation.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now { get; } = DateTime.Now;

    public DateOnly CalculateDateInCurrentWeek(DayOfWeek dayOfWeek)
    {
        var firstDayOfUpcomingWeek = Now.GetFirstDayOfWeek();
        return dayOfWeek switch
        {
            DayOfWeek.Monday => firstDayOfUpcomingWeek,
            DayOfWeek.Tuesday => firstDayOfUpcomingWeek.AddDays(1),
            DayOfWeek.Wednesday => firstDayOfUpcomingWeek.AddDays(2),
            DayOfWeek.Thursday => firstDayOfUpcomingWeek.AddDays(3),
            DayOfWeek.Friday => firstDayOfUpcomingWeek.AddDays(4),
            DayOfWeek.Saturday => firstDayOfUpcomingWeek.AddDays(5),
            DayOfWeek.Sunday => firstDayOfUpcomingWeek.AddDays(6)
        };
    }

    public DateTime CalculateDateTimeInCurrentWeek(DayOfWeek dayOfWeek, TimeOnly timeOnly) => 
        CalculateDateInCurrentWeek(dayOfWeek).ToDateTime(timeOnly);
}